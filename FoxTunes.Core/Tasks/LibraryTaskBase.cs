﻿#pragma warning disable 612, 618
using FoxDb;
using FoxDb.Interfaces;
using FoxTunes.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoxTunes
{
    public abstract class LibraryTaskBase : BackgroundTask
    {
        public const string ID = "B6AF297E-F334-481D-8D60-BD5BE5935BD9";

        protected LibraryTaskBase()
            : base(ID)
        {
            this.Warnings = new Dictionary<LibraryItem, IList<string>>();
        }

        public IDictionary<LibraryItem, IList<string>> Warnings { get; private set; }

        public ICore Core { get; private set; }

        public IDatabaseComponent Database { get; private set; }

        public IPlaybackManager PlaybackManager { get; private set; }

        public IMetaDataSourceFactory MetaDataSourceFactory { get; private set; }

        public ISignalEmitter SignalEmitter { get; private set; }

        public override void InitializeComponent(ICore core)
        {
            this.Core = core;
            this.Database = core.Factories.Database.Create();
            this.PlaybackManager = core.Managers.Playback;
            this.MetaDataSourceFactory = core.Factories.MetaDataSource;
            this.SignalEmitter = core.Components.SignalEmitter;
            base.InitializeComponent(core);
        }

        protected virtual async Task<IEnumerable<string>> GetRoots()
        {
            using (var transaction = this.Database.BeginTransaction(this.Database.PreferredIsolationLevel))
            {
                using (var sequence = this.Database.Set<LibraryRoot>(transaction).GetAsyncEnumerator())
                {
                    var result = new List<string>();
                    while (await sequence.MoveNextAsync().ConfigureAwait(false))
                    {
                        result.Add(sequence.Current.DirectoryName);
                    }
                    return result;
                }
            }
        }

        protected virtual async Task AddRoots(IEnumerable<string> paths)
        {
            paths = await this.NormalizeRoots(paths).ConfigureAwait(false);
            using (var transaction = this.Database.BeginTransaction(this.Database.PreferredIsolationLevel))
            {
                var set = this.Database.Set<LibraryRoot>(transaction);
                Logger.Write(this, LogLevel.Debug, "Clearing library roots.");
                await set.ClearAsync().ConfigureAwait(false);
                foreach (var path in paths)
                {
                    Logger.Write(this, LogLevel.Debug, "Creating library root: {0}", path);
                    await set.AddAsync(
                        set.Create().With(
                            libraryRoot => libraryRoot.DirectoryName = path
                        )
                    ).ConfigureAwait(false);
                }
                if (transaction.HasTransaction)
                {
                    transaction.Commit();
                }
            }
        }

        protected virtual async Task<IEnumerable<string>> NormalizeRoots(IEnumerable<string> newPaths)
        {
            var currentPaths = await this.GetRoots().ConfigureAwait(false);
            return LibraryRoot.Normalize(currentPaths, newPaths);
        }

        protected virtual async Task ClearRoots()
        {
            using (var transaction = this.Database.BeginTransaction(this.Database.PreferredIsolationLevel))
            {
                var set = this.Database.Set<LibraryRoot>(transaction);
                Logger.Write(this, LogLevel.Debug, "Clearing library roots.");
                await set.ClearAsync().ConfigureAwait(false);
                transaction.Commit();
            }
        }

        protected virtual async Task AddPaths(IEnumerable<string> paths)
        {
            using (var task = new SingletonReentrantTask(this, ComponentSlots.Database, SingletonReentrantTask.PRIORITY_LOW, async cancellationToken =>
             {
                 await this.AddLibraryItems(paths, cancellationToken).ConfigureAwait(false);
                 if (cancellationToken.IsCancellationRequested)
                 {
                     this.Name = "Waiting..";
                     this.Description = string.Empty;
                 }
             }))
            {
                await task.Run().ConfigureAwait(false);
            }
            if (this.IsCancellationRequested)
            {
                //Reset cancellation as the next phases should finish quickly.
                //Cancelling again will still work.
                this.IsCancellationRequested = false;
            }
            using (var task = new SingletonReentrantTask(this, ComponentSlots.Database, SingletonReentrantTask.PRIORITY_LOW, async cancellationToken =>
            {
                await this.AddOrUpdateMetaData(cancellationToken).ConfigureAwait(false);
                if (cancellationToken.IsCancellationRequested)
                {
                    this.Name = "Waiting..";
                    this.Description = string.Empty;
                }
            }))
            {
                await task.Run().ConfigureAwait(false);
            }
            if (this.IsCancellationRequested)
            {
                //Reset cancellation as the next phases should finish quickly.
                //Cancelling again will still work.
                this.IsCancellationRequested = false;
            }
            await this.BuildHierarchies(LibraryItemStatus.Import).ConfigureAwait(false);
            await RemoveCancelledLibraryItems(this.Database).ConfigureAwait(false);
            await SetLibraryItemsStatus(this.Database, LibraryItemStatus.None).ConfigureAwait(false);
        }

        protected virtual async Task AddLibraryItems(IEnumerable<string> paths, CancellationToken cancellationToken)
        {
            //We don't know how many files we're about to enumerate.
            this.IsIndeterminate = true;

            using (var transaction = this.Database.BeginTransaction(this.Database.PreferredIsolationLevel))
            {
                using (var libraryPopulator = new LibraryPopulator(this.Database, this.PlaybackManager, this.Visible, transaction))
                {
                    libraryPopulator.InitializeComponent(this.Core);
                    await this.WithSubTask(libraryPopulator,
                         () => libraryPopulator.Populate(paths, cancellationToken)
                    ).ConfigureAwait(false);
                }
                transaction.Commit();
            }
        }

        protected virtual async Task AddOrUpdateMetaData(CancellationToken cancellationToken)
        {
            using (var transaction = this.Database.BeginTransaction(this.Database.PreferredIsolationLevel))
            {
                using (var metaDataPopulator = new LibraryMetaDataPopulator(this.Database, this.Visible, transaction))
                {
                    metaDataPopulator.InitializeComponent(this.Core);
                    await this.WithSubTask(metaDataPopulator,
                        () => metaDataPopulator.Populate(LibraryItemStatus.Import, cancellationToken)
                    ).ConfigureAwait(false);
                    foreach (var pair in metaDataPopulator.Warnings)
                    {
                        if (pair.Key is LibraryItem libraryItem)
                        {
                            this.Warnings.GetOrAdd(libraryItem, _libraryItem => new List<string>()).AddRange(pair.Value);
                        }
                    }
                }
                transaction.Commit();
            }
        }

        protected virtual async Task BuildHierarchies(LibraryItemStatus? status)
        {
            using (var task = new SingletonReentrantTask(this, ComponentSlots.Database, SingletonReentrantTask.PRIORITY_LOW, async cancellationToken =>
            {
                using (var transaction = this.Database.BeginTransaction(this.Database.PreferredIsolationLevel))
                {
                    await this.AddHiearchies(status, cancellationToken, transaction).ConfigureAwait(false);
                    if (cancellationToken.IsCancellationRequested)
                    {
                        this.Name = "Waiting..";
                        this.Description = string.Empty;
                    }
                    else
                    {
                        this.Name = "Finalizing";
                        this.IsIndeterminate = true;
                    }
                    transaction.Commit();
                }
            }))
            {
                await task.Run().ConfigureAwait(false);
            }
        }

        private async Task AddHiearchies(LibraryItemStatus? status, CancellationToken cancellationToken, ITransactionSource transaction)
        {
            using (var libraryHierarchyPopulator = new LibraryHierarchyPopulator(this.Database, this.Visible, transaction))
            {
                libraryHierarchyPopulator.InitializeComponent(this.Core);
                await this.WithSubTask(libraryHierarchyPopulator,
                    () => libraryHierarchyPopulator.Populate(status, cancellationToken)
                ).ConfigureAwait(false);
            }
        }

        protected virtual async Task RemoveHierarchies(LibraryItemStatus? status)
        {
            using (var transaction = this.Database.BeginTransaction(this.Database.PreferredIsolationLevel))
            {
                await this.Database.ExecuteAsync(this.Database.Queries.RemoveLibraryHierarchyItems, (parameters, phase) =>
                {
                    switch (phase)
                    {
                        case DatabaseParameterPhase.Fetch:
                            parameters["status"] = status;
                            break;
                    }
                }, transaction).ConfigureAwait(false);
                transaction.Commit();
            }
        }

        protected virtual async Task RemoveItems(LibraryItemStatus? status)
        {
            using (var transaction = this.Database.BeginTransaction(this.Database.PreferredIsolationLevel))
            {
                await this.Database.ExecuteAsync(this.Database.Queries.RemoveLibraryItems, (parameters, phase) =>
                {
                    switch (phase)
                    {
                        case DatabaseParameterPhase.Fetch:
                            parameters["status"] = status;
                            break;
                    }
                }, transaction).ConfigureAwait(false);
                transaction.Commit();
            }
        }

        public static async Task RemoveCancelledLibraryItems(IDatabaseComponent database)
        {
            using (var transaction = database.BeginTransaction(database.PreferredIsolationLevel))
            {
                await database.ExecuteAsync(database.Queries.RemoveCancelledLibraryItems).ConfigureAwait(false);
            }
        }

        public static async Task SetLibraryItemsStatus(IDatabaseComponent database, LibraryItemStatus status)
        {
            var query = database.QueryFactory.Build();
            query.Update.SetTable(database.Tables.LibraryItem);
            query.Update.AddColumn(database.Tables.LibraryItem.Column("Status"));
            using (var transaction = database.BeginTransaction(database.PreferredIsolationLevel))
            {
                await database.ExecuteAsync(query, (parameters, phase) =>
                {
                    switch (phase)
                    {
                        case DatabaseParameterPhase.Fetch:
                            parameters["status"] = status;
                            break;
                    }
                }, transaction).ConfigureAwait(false);
                transaction.Commit();
            }
        }

        public static async Task SetLibraryItemImportDate(IDatabaseComponent database, LibraryItem libraryItem, DateTime importDate)
        {
            using (var transaction = database.BeginTransaction(database.PreferredIsolationLevel))
            {
                await SetLibraryItemImportDate(database, libraryItem, importDate, transaction).ConfigureAwait(false);
                transaction.Commit();
            }
        }

        public static async Task SetLibraryItemImportDate(IDatabaseComponent database, LibraryItem libraryItem, DateTime importDate, ITransactionSource transaction)
        {
            var table = database.Tables.LibraryItem;
            var builder = database.QueryFactory.Build();
            builder.Update.SetTable(table);
            builder.Update.AddColumn(table.Column("ImportDate"));
            builder.Filter.AddColumn(table.PrimaryKey);
            //This is kind of dumb but it seems that file access times "lag" a little.
            importDate = importDate.AddSeconds(30);
            await database.ExecuteAsync(builder, (parameters, phase) =>
            {
                switch (phase)
                {
                    case DatabaseParameterPhase.Fetch:
                        parameters["id"] = libraryItem.Id;
                        parameters["importDate"] = DateTimeHelper.ToString(importDate);
                        break;
                }
            }, transaction).ConfigureAwait(false);
            libraryItem.SetImportDate(importDate);
        }

        public static async Task SetLibraryItemStatus(IDatabaseComponent database, int libraryItemId, LibraryItemStatus status)
        {
            var builder = database.QueryFactory.Build();
            builder.Update.AddColumn(database.Tables.LibraryItem.Column("Status"));
            builder.Update.SetTable(database.Tables.LibraryItem);
            builder.Filter.AddColumn(database.Tables.LibraryItem.Column("Id"));
            using (var transaction = database.BeginTransaction(database.PreferredIsolationLevel))
            {
                await database.ExecuteAsync(builder, (parameters, phase) =>
                {
                    switch (phase)
                    {
                        case DatabaseParameterPhase.Fetch:
                            parameters["status"] = status;
                            parameters["id"] = libraryItemId;
                            break;
                    }
                }, transaction).ConfigureAwait(false);
                transaction.Commit();
            }
        }

        protected override void OnDisposing()
        {
            if (this.Database != null)
            {
                this.Database.Dispose();
            }
            base.OnDisposing();
        }
    }
}
