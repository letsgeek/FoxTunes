﻿using FoxTunes.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FoxTunes
{
    public abstract class LyricsProvider : StandardComponent
    {
        protected LyricsProvider(string id, string name = null, string description = null)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
        }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public LyricsBehaviour Behaviour { get; private set; }

        public IMetaDataManager MetaDataManager { get; private set; }

        public override void InitializeComponent(ICore core)
        {
            this.Behaviour = ComponentRegistry.Instance.GetComponent<LyricsBehaviour>();
            this.MetaDataManager = core.Managers.MetaData;
            base.InitializeComponent(core);
        }

        public abstract string None { get; }

        public abstract Task<LyricsResult> Lookup(IFileData fileData);

        protected virtual async Task SaveMetaData(IFileData fileData, string releaseId)
        {
            lock (fileData.MetaDatas)
            {
                var metaDataItem = fileData.MetaDatas.FirstOrDefault(
                    element => string.Equals(element.Name, CustomMetaData.LyricsRelease, StringComparison.OrdinalIgnoreCase) && element.Type == MetaDataItemType.Tag
                );
                if (metaDataItem == null)
                {
                    metaDataItem = new MetaDataItem(CustomMetaData.LyricsRelease, MetaDataItemType.Tag);
                    fileData.MetaDatas.Add(metaDataItem);
                }
                metaDataItem.Value = releaseId;
            }
            if (fileData is LibraryItem libraryItem)
            {
                await this.MetaDataManager.Save(
                    new[] { libraryItem },
                    false, //These tags cannot be "written".
                    false,
                    new[] { CustomMetaData.LyricsRelease }
                ).ConfigureAwait(false);
            }
            if (fileData is PlaylistItem playlistItem)
            {
                await this.MetaDataManager.Save(
                    new[] { playlistItem },
                    false, //These tags cannot be "written".
                    false,
                    new[] { CustomMetaData.LyricsRelease }
                ).ConfigureAwait(false);
            }
        }
    }
}
