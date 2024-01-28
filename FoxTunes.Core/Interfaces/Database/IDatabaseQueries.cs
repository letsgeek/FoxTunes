﻿using FoxDb.Interfaces;
using System.Collections.Generic;

namespace FoxTunes.Interfaces
{
    public interface IDatabaseQueries : IBaseComponent
    {
        IDatabaseQuery RemovePlaylistItems { get; }

        IDatabaseQuery RemoveLibraryItems { get; }

        IDatabaseQuery AddLibraryHierarchyNodeToPlaylist { get; }

        IDatabaseQuery AddPlaylistSequenceRecord { get; }

        IDatabaseQuery AddLibraryMetaDataItems { get; }

        IDatabaseQuery AddPlaylistMetaDataItems { get; }

        IDatabaseQuery GetLibraryHierarchyMetaDataItems { get; }

        IDatabaseQuery GetLibraryHierarchyNodes { get; }

        IDatabaseQuery GetLibraryHierarchyNodesWithFilter { get; }

        IDatabaseQuery UpdatePlaylistVariousArtists { get; }

        IDatabaseQuery UpdateLibraryVariousArtists { get; }

        IDatabaseQuery BeginSequencePlaylistItems { get; }

        IDatabaseQuery SequencePlaylistItems(IEnumerable<string> metaDataNames);

        IDatabaseQuery EndSequencePlaylistItems { get; }

        IDatabaseQuery BeginBuildLibraryHierarchies { get; }

        IDatabaseQuery BuildLibraryHierarchies(IEnumerable<string> metaDataNames);

        IDatabaseQuery EndBuildLibraryHierarchies { get; }
    }
}
