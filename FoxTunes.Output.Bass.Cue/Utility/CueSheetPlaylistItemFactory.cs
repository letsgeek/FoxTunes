﻿using FoxTunes.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FoxTunes
{
    public class CueSheetPlaylistItemFactory : BaseComponent
    {
        private static IDictionary<string, string> FILE_NAMES = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "PERFORMER", CommonMetaData.Artist },
            { "DATE", CommonMetaData.Year },
            { "TITLE", CommonMetaData.Album }
        };

        private static IDictionary<string, string> TRACK_NAMES = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "PERFORMER", CommonMetaData.Artist },
            { "DATE", CommonMetaData.Year }
        };

        public IMetaDataSourceFactory MetaDataSourceFactory { get; private set; }

        public override void InitializeComponent(ICore core)
        {
            this.MetaDataSourceFactory = core.Factories.MetaDataSource;
            base.InitializeComponent(core);
        }

        public async Task<PlaylistItem[]> Create(CueSheet cueSheet)
        {
            var playlistItems = new List<PlaylistItem>();
            var directoryName = Path.GetDirectoryName(cueSheet.FileName);
            var metaDataSource = this.MetaDataSourceFactory.Create();
            for (var a = 0; a < cueSheet.Files.Length; a++)
            {
                var file = cueSheet.Files[a];
                var metaData = default(IEnumerable<MetaDataItem>);
                try
                {
                    metaData = (
                        await metaDataSource.GetMetaData(Path.Combine(directoryName, file.Path)).ConfigureAwait(false)
                    ).ToArray();
                }
                catch (Exception e)
                {
                    metaData = Enumerable.Empty<MetaDataItem>();
                    Logger.Write(this, LogLevel.Debug, "Failed to read meta data from file \"{0}\": {1}", file.Path, e.Message);
                }
                for (var b = 0; b < file.Tracks.Length; b++)
                {
                    var fileName = default(string);
                    if (b + 1 < file.Tracks.Length)
                    {
                        fileName = BassCueStreamAdvisor.CreateUrl(
                            Path.Combine(directoryName, file.Path),
                            file.Tracks[b].Index.Time,
                            CueSheet.GetTrackLength(file.Tracks[b], file.Tracks[b + 1])
                        );
                    }
                    else
                    {
                        fileName = BassCueStreamAdvisor.CreateUrl(
                            Path.Combine(directoryName, file.Path),
                            file.Tracks[b].Index.Time
                        );
                    }
                    var playlistItem = new PlaylistItem()
                    {
                        DirectoryName = directoryName,
                        FileName = fileName
                    };
                    playlistItem.MetaDatas = this.GetMetaData(cueSheet, file.Tracks[b], metaData);
                    playlistItems.Add(playlistItem);
                }
            }
            return playlistItems.ToArray();
        }

        protected virtual MetaDataItem[] GetMetaData(CueSheet cueSheet, CueSheetTrack cueSheetTrack, IEnumerable<MetaDataItem> fileMetaData)
        {
            var metaDataItems = new Dictionary<string, MetaDataItem>(StringComparer.OrdinalIgnoreCase);
            if (fileMetaData != null)
            {
                foreach (var metaDataItem in fileMetaData)
                {
                    metaDataItems[metaDataItem.Name] = metaDataItem;
                }
            }
            foreach (var tag in cueSheet.Tags)
            {
                var name = default(string);
                if (!FILE_NAMES.TryGetValue(tag.Name, out name))
                {
                    if (!CommonMetaData.Lookup.TryGetValue(tag.Name, out name))
                    {
                        name = tag.Name;
                    }
                }
                metaDataItems[name] = new MetaDataItem(name, MetaDataItemType.Tag)
                {
                    Value = tag.Value
                };
            }
            foreach (var tag in cueSheetTrack.Tags)
            {
                var name = default(string);
                if (!TRACK_NAMES.TryGetValue(tag.Name, out name))
                {
                    if (!CommonMetaData.Lookup.TryGetValue(tag.Name, out name))
                    {
                        name = tag.Name;
                    }
                }
                metaDataItems[name] = new MetaDataItem(name, MetaDataItemType.Tag)
                {
                    Value = tag.Value
                };
            }
            metaDataItems[CommonMetaData.Track] = new MetaDataItem(CommonMetaData.Track, MetaDataItemType.Tag)
            {
                Value = cueSheetTrack.Number
            };
            //TODO: We could create the CommonProperties.Duration for all but the last track in each file. 
            //TODO: Without understanding the file format we can't determine the length of the last track.
            //TODO: Just don't provide any duration for now.
            metaDataItems.Remove(CommonProperties.Duration);
            return metaDataItems.Values.ToArray();
        }
    }
}
