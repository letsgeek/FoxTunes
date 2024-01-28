﻿using FoxTunes.Interfaces;
using FoxTunes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FoxTunes
{
    [Component("8080D985-D642-4189-901B-A84530A1F110", ComponentSlots.None, priority: ComponentAttribute.PRIORITY_HIGH)]
    [WindowsUserInterfaceDependency]
    public class LibraryBrowserTileBrushFactory : StandardFactory, IDisposable
    {
        public LibraryBrowserTileProvider LibraryBrowserTileProvider { get; private set; }

        public PixelSizeConverter PixelSizeConverter { get; private set; }

        public ImageLoader ImageLoader { get; private set; }

        public ArtworkPlaceholderBrushFactory PlaceholderBrushFactory { get; private set; }

        public ILibraryHierarchyBrowser LibraryHierarchyBrowser { get; private set; }

        public ISignalEmitter SignalEmitter { get; private set; }

        public IConfiguration Configuration { get; private set; }

        public IntegerConfigurationElement TileSize { get; private set; }

        public TaskFactory Factory { get; private set; }

        public CappedDictionary<LibraryHierarchyNode, ImageBrush> Store { get; private set; }

        public override void InitializeComponent(ICore core)
        {
            this.LibraryBrowserTileProvider = ComponentRegistry.Instance.GetComponent<LibraryBrowserTileProvider>();
            this.PixelSizeConverter = ComponentRegistry.Instance.GetComponent<PixelSizeConverter>();
            this.ImageLoader = ComponentRegistry.Instance.GetComponent<ImageLoader>();
            this.PlaceholderBrushFactory = ComponentRegistry.Instance.GetComponent<ArtworkPlaceholderBrushFactory>();
            this.LibraryHierarchyBrowser = core.Components.LibraryHierarchyBrowser;
            this.SignalEmitter = core.Components.SignalEmitter;
            this.SignalEmitter.Signal += this.OnSignal;
            this.Configuration = core.Components.Configuration;
            this.TileSize = this.Configuration.GetElement<IntegerConfigurationElement>(
                WindowsUserInterfaceConfiguration.SECTION,
                LibraryBrowserBehaviourConfiguration.LIBRARY_BROWSER_TILE_SIZE
            );
            this.Configuration.GetElement<IntegerConfigurationElement>(
                ImageBehaviourConfiguration.SECTION,
                ImageLoaderConfiguration.THREADS
            ).ConnectValue(value => this.CreateTaskFactory(value));
            this.Configuration.GetElement<IntegerConfigurationElement>(
                ImageBehaviourConfiguration.SECTION,
                ImageLoaderConfiguration.CACHE_SIZE
            ).ConnectValue(value => this.CreateCache(value));
            base.InitializeComponent(core);
        }

        protected virtual Task OnSignal(object sender, ISignal signal)
        {
            switch (signal.Name)
            {
                case CommonSignals.MetaDataUpdated:
                    var names = signal.State as IEnumerable<string>;
                    this.Reset(names);
                    break;
                case CommonSignals.ImagesUpdated:
                    Logger.Write(this, LogLevel.Debug, "Images were updated, resetting cache.");
                    this.Reset();
                    break;
            }
#if NET40
            return TaskEx.FromResult(false);
#else
            return Task.CompletedTask;
#endif
        }

        public AsyncResult<ImageBrush> Create(LibraryHierarchyNode libraryHierarchyNode)
        {
            var width = this.TileSize.Value;
            var height = this.TileSize.Value;
            this.PixelSizeConverter.Convert(ref width, ref height);
            var placeholder = this.PlaceholderBrushFactory.Create(width, height);
            if (libraryHierarchyNode == null)
            {
                return AsyncResult<ImageBrush>.FromValue(placeholder);
            }
            var cache = string.IsNullOrEmpty(this.LibraryHierarchyBrowser.Filter);
            if (cache)
            {
                var brush = default(ImageBrush);
                if (this.Store.TryGetValue(libraryHierarchyNode, out brush))
                {
                    if (brush != null)
                    {
                        return AsyncResult<ImageBrush>.FromValue(brush);
                    }
                    else
                    {
                        return AsyncResult<ImageBrush>.FromValue(placeholder);
                    }
                }
            }
            return new AsyncResult<ImageBrush>(placeholder, this.Factory.StartNew(() =>
            {
                if (cache)
                {
                    return this.Store.GetOrAdd(
                        libraryHierarchyNode,
                        () => this.Create(libraryHierarchyNode, width, height, true)
                    );
                }
                else
                {
                    return this.Create(libraryHierarchyNode, width, height, false);
                }
            }));
        }

        protected virtual ImageBrush Create(LibraryHierarchyNode libraryHierarchyNode, int width, int height, bool cache)
        {
            Logger.Write(this, LogLevel.Debug, "Creating brush: {0}x{1}", width, height);
            var source = this.LibraryBrowserTileProvider.CreateImageSource(
                libraryHierarchyNode,
                width,
                height,
                cache
            );
            if (source == null)
            {
                return null;
            }
            var brush = new ImageBrush(source)
            {
                Stretch = Stretch.Uniform
            };
            brush.Freeze();
            return brush;
        }

        protected virtual void CreateTaskFactory(int threads)
        {
            Logger.Write(this, LogLevel.Debug, "Creating task factory for {0} threads.", threads);
            this.Factory = new TaskFactory(new TaskScheduler(new ParallelOptions()
            {
                MaxDegreeOfParallelism = threads
            }));
        }

        protected virtual void CreateCache(int capacity)
        {
            Logger.Write(this, LogLevel.Debug, "Creating cache for {0} items.", capacity);
            this.Store = new CappedDictionary<LibraryHierarchyNode, ImageBrush>(capacity);
        }

        protected virtual void Reset(IEnumerable<string> names)
        {
            if (names != null && names.Any())
            {
                if (!names.Contains(CommonImageTypes.FrontCover, StringComparer.OrdinalIgnoreCase))
                {
                    return;
                }
            }
            Logger.Write(this, LogLevel.Debug, "Meta data was updated, resetting cache.");
            this.Reset();
        }

        protected virtual void Reset()
        {
            this.Store.Clear();
        }

        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.IsDisposed || !disposing)
            {
                return;
            }
            this.OnDisposing();
            this.IsDisposed = true;
        }

        protected virtual void OnDisposing()
        {
            if (this.SignalEmitter != null)
            {
                this.SignalEmitter.Signal -= this.OnSignal;
            }
        }

        ~LibraryBrowserTileBrushFactory()
        {
            Logger.Write(this.GetType(), LogLevel.Error, "Component was not disposed: {0}", this.GetType().Name);
            try
            {
                this.Dispose(true);
            }
            catch
            {
                //Nothing can be done, never throw on GC thread.
            }
        }
    }
}
