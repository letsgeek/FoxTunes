﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FoxTunes {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FoxTunes.DB.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to WITH
        ///&quot;LibraryHierarchyItems_Lookup&quot; AS
        ///(
        ///	SELECT * 
        ///	FROM [LibraryHierarchyItems]
        ///	WHERE ((@parentId IS NULL AND [Parent_Id] IS NULL) OR [Parent_Id] = @parentId)
        ///		AND [LibraryHierarchy_Id] = @libraryHierarchyId
        ///		AND [Value] = @value
        ///		AND [IsLeaf] = @isLeaf
        ///)
        ///
        ///INSERT INTO [LibraryHierarchyItems] ([Parent_Id], [LibraryHierarchy_Id], [Value], [IsLeaf])
        ///SELECT @parentId, @libraryHierarchyId, @value, @isLeaf
        ///WHERE NOT EXISTS(SELECT * FROM &quot;LibraryHierarchyItems_Lookup&quot;);
        ///
        ///WITH
        ///&quot;LibraryHierarch [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string AddLibraryHierarchyNode {
            get {
                return ResourceManager.GetString("AddLibraryHierarchyNode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO &quot;PlaylistItems&quot; (&quot;LibraryItem_Id&quot;, &quot;Sequence&quot;, &quot;DirectoryName&quot;, &quot;FileName&quot;, &quot;Status&quot;) 
        ///SELECT &quot;LibraryItems&quot;.&quot;Id&quot;, @sequence, &quot;LibraryItems&quot;.&quot;DirectoryName&quot;, &quot;LibraryItems&quot;.&quot;FileName&quot;, @status
        ///FROM &quot;LibraryHierarchyItems&quot;
        ///	JOIN &quot;LibraryHierarchyItem_LibraryItem&quot; 
        ///		ON &quot;LibraryHierarchyItems&quot;.&quot;Id&quot; = &quot;LibraryHierarchyItem_LibraryItem&quot;.&quot;LibraryHierarchyItem_Id&quot;
        ///	JOIN &quot;LibraryItems&quot;
        ///		ON &quot;LibraryItems&quot;.&quot;Id&quot; = &quot;LibraryHierarchyItem_LibraryItem&quot;.&quot;LibraryItem_Id&quot;
        ///WHERE &quot;LibraryHierarchyItems&quot;.&quot;I [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string AddLibraryHierarchyNodeToPlaylist {
            get {
                return ResourceManager.GetString("AddLibraryHierarchyNodeToPlaylist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO &quot;LibraryItem_MetaDataItem&quot; (&quot;LibraryItem_Id&quot;, &quot;MetaDataItem_Id&quot;)
        ///SELECT @itemId, @metaDataItemId
        ///WHERE NOT EXISTS(
        ///	SELECT *
        ///	FROM &quot;LibraryItem_MetaDataItem&quot; 
        ///	WHERE &quot;LibraryItem_Id&quot; = @itemId AND &quot;MetaDataItem_Id&quot; = @metaDataItemId
        ///);.
        /// </summary>
        internal static string AddLibraryMetaDataItem {
            get {
                return ResourceManager.GetString("AddLibraryMetaDataItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO &quot;PlaylistItem_MetaDataItem&quot; (&quot;PlaylistItem_Id&quot;, &quot;MetaDataItem_Id&quot;)
        ///SELECT @itemId, @metaDataItemId
        ///WHERE NOT EXISTS(
        ///	SELECT *
        ///	FROM &quot;PlaylistItem_MetaDataItem&quot; 
        ///	WHERE &quot;PlaylistItem_Id&quot; = @itemId AND &quot;MetaDataItem_Id&quot; = @metaDataItemId
        ///);.
        /// </summary>
        internal static string AddPlaylistMetaDataItem {
            get {
                return ResourceManager.GetString("AddPlaylistMetaDataItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to WITH 
        ///PopulatedNodes AS
        ///(
        ///	SELECT [LibraryHierarchyItems].[Id], [LibraryHierarchyItems].[Value]
        ///	FROM [LibraryHierarchyItems]
        ///		JOIN [LibraryHierarchyItems] Children
        ///			ON [LibraryHierarchyItems].[Id] = Children.[Parent_Id]
        ///	GROUP BY [LibraryHierarchyItems].[Id], [LibraryHierarchyItems].[Value]
        ///	HAVING COUNT(Children.[Id]) &gt; 1
        ///),
        ///
        ///DefunctNodes(Id, Parent_Id, Value) AS
        ///(
        ///	SELECT [LibraryHierarchyItems].[Id], [LibraryHierarchyItems].[Parent_Id], [LibraryHierarchyItems].[Value]
        ///	FROM [LibraryHier [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CleanupLibraryHierarchyNodes {
            get {
                return ResourceManager.GetString("CleanupLibraryHierarchyNodes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE FROM &quot;LibraryItem_MetaDataItem&quot;
        ///WHERE &quot;Id&quot; IN
        ///(
        ///	SELECT &quot;LibraryItem_MetaDataItem&quot;.&quot;Id&quot;
        ///	FROM &quot;LibraryItem_MetaDataItem&quot;
        ///		JOIN &quot;MetaDataItems&quot;
        ///			ON &quot;LibraryItem_MetaDataItem&quot;.&quot;MetaDataItem_Id&quot; = &quot;MetaDataItems&quot;.&quot;Id&quot;
        ///				AND (@type &amp; &quot;MetaDataItems&quot;.&quot;Type&quot;) =  &quot;MetaDataItems&quot;.&quot;Type&quot;
        ///	WHERE &quot;LibraryItem_MetaDataItem&quot;.&quot;LibraryItem_Id&quot; = @itemId
        ///).
        /// </summary>
        internal static string ClearLibraryMetaDataItems {
            get {
                return ResourceManager.GetString("ClearLibraryMetaDataItems", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE FROM &quot;PlaylistItem_MetaDataItem&quot;
        ///WHERE &quot;Id&quot; IN
        ///(
        ///	SELECT &quot;PlaylistItem_MetaDataItem&quot;.&quot;Id&quot;
        ///	FROM &quot;PlaylistItem_MetaDataItem&quot;
        ///		JOIN &quot;MetaDataItems&quot;
        ///			ON &quot;PlaylistItem_MetaDataItem&quot;.&quot;MetaDataItem_Id&quot; = &quot;MetaDataItems&quot;.&quot;Id&quot;
        ///				AND (@type &amp; &quot;MetaDataItems&quot;.&quot;Type&quot;) =  &quot;MetaDataItems&quot;.&quot;Type&quot;
        ///	WHERE &quot;PlaylistItem_MetaDataItem&quot;.&quot;PlaylistItem_Id&quot; = @itemId
        ///).
        /// </summary>
        internal static string ClearPlaylistMetaDataItems {
            get {
                return ResourceManager.GetString("ClearPlaylistMetaDataItems", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT CASE WHEN EXISTS(
        ///	SELECT *
        ///	FROM &quot;LibraryItems&quot;
        ///		JOIN &quot;LibraryHierarchyItem_LibraryItem&quot;
        ///			ON &quot;LibraryItems&quot;.&quot;Id&quot; = &quot;LibraryHierarchyItem_LibraryItem&quot;.&quot;LibraryItem_Id&quot;
        ///	WHERE &quot;LibraryHierarchyItem_LibraryItem&quot;.&quot;LibraryHierarchyItem_Id&quot; = @libraryHierarchyItemId
        ///		AND &quot;LibraryItems&quot;.&quot;Favorite&quot; = 0
        ///) THEN 0 ELSE 1 END.
        /// </summary>
        internal static string GetIsFavorite {
            get {
                return ResourceManager.GetString("GetIsFavorite", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT &quot;LibraryHierarchyItems&quot;.&quot;Id&quot;, &quot;LibraryHierarchy_Id&quot;, &quot;Value&quot;, &quot;IsLeaf&quot;
        ///FROM &quot;LibraryHierarchyItems&quot;
        ///WHERE &quot;LibraryHierarchy_Id&quot; = @libraryHierarchyId
        ///	AND ((@libraryHierarchyItemId IS NULL AND &quot;LibraryHierarchyItems&quot;.&quot;Parent_Id&quot; IS NULL) 
        ///		OR &quot;LibraryHierarchyItems&quot;.&quot;Parent_Id&quot; = @libraryHierarchyItemId)
        ///	AND (@favorite IS NULL OR EXISTS(
        ///		SELECT * 
        ///		FROM &quot;LibraryItems&quot; 
        ///			JOIN &quot;LibraryHierarchyItem_LibraryItem&quot; 
        ///				ON &quot;LibraryHierarchyItem_LibraryItem&quot;.&quot;LibraryItem_Id&quot; = &quot;LibraryItems&quot; [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetLibraryHierarchyNodes {
            get {
                return ResourceManager.GetString("GetLibraryHierarchyNodes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to WITH
        ///
        ///LibraryHierarchyParents(&quot;Root&quot;, &quot;Id&quot;, &quot;Parent_Id&quot;, &quot;Value&quot;)
        ///AS
        ///(
        ///	SELECT &quot;LibraryHierarchyItems&quot;.&quot;Id&quot;, &quot;LibraryHierarchyItems&quot;.&quot;Id&quot;, &quot;LibraryHierarchyItems&quot;.&quot;Parent_Id&quot;, &quot;LibraryHierarchyItems&quot;.&quot;Value&quot;
        ///	FROM &quot;LibraryHierarchyItems&quot;
        ///	WHERE &quot;LibraryHierarchy_Id&quot; = @libraryHierarchyId
        ///		AND ((@libraryHierarchyItemId IS NULL AND &quot;LibraryHierarchyItems&quot;.&quot;Parent_Id&quot; IS NULL) OR &quot;LibraryHierarchyItems&quot;.&quot;Parent_Id&quot; = @libraryHierarchyItemId)
        ///	UNION ALL 
        ///	SELECT &quot;Root&quot;, &quot;LibraryHierarchyItems&quot;.&quot;Id&quot;,  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetLibraryHierarchyNodesWithFilter {
            get {
                return ResourceManager.GetString("GetLibraryHierarchyNodesWithFilter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT 
        ///	&quot;LibraryItems&quot;.&quot;Id&quot; AS &quot;LibraryItems_Id&quot;,
        ///	&quot;LibraryItems&quot;.&quot;DirectoryName&quot; AS &quot;LibraryItems_DirectoryName&quot;,
        ///	&quot;LibraryItems&quot;.&quot;FileName&quot; AS &quot;LibraryItems_FileName&quot;,
        ///	&quot;LibraryItems&quot;.&quot;ImportDate&quot; AS &quot;LibraryItems_ImportDate&quot;,
        ///	&quot;LibraryItems&quot;.&quot;Favorite&quot; AS &quot;LibraryItems_Favorite&quot;,
        ///	&quot;LibraryItems&quot;.&quot;Status&quot; AS &quot;LibraryItems_Status&quot;,
        ///	&quot;MetaDataItems&quot;.&quot;Id&quot; AS &quot;MetaDataItems_Id&quot;,
        ///	&quot;MetaDataItems&quot;.&quot;Name&quot; AS &quot;MetaDataItems_Name&quot;,
        ///	&quot;MetaDataItems&quot;.&quot;Type&quot; AS &quot;MetaDataItems_Type&quot;,
        ///	&quot;MetaDataItems&quot;.&quot;Value [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetLibraryItems {
            get {
                return ResourceManager.GetString("GetLibraryItems", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to WITH
        ///
        ///LibraryHierarchyParents(&quot;Root&quot;, &quot;Id&quot;, &quot;Parent_Id&quot;, &quot;Value&quot;)
        ///AS
        ///(
        ///	SELECT &quot;LibraryHierarchyItems&quot;.&quot;Id&quot;, &quot;LibraryHierarchyItems&quot;.&quot;Id&quot;, &quot;LibraryHierarchyItems&quot;.&quot;Parent_Id&quot;, &quot;LibraryHierarchyItems&quot;.&quot;Value&quot;
        ///	FROM &quot;LibraryHierarchyItems&quot;
        ///	WHERE &quot;LibraryHierarchy_Id&quot; = @libraryHierarchyId
        ///		AND &quot;LibraryHierarchyItems&quot;.&quot;Id&quot; = @libraryHierarchyItemId
        ///	UNION ALL 
        ///	SELECT &quot;Root&quot;, &quot;LibraryHierarchyItems&quot;.&quot;Id&quot;, &quot;LibraryHierarchyItems&quot;.&quot;Parent_Id&quot;, &quot;LibraryHierarchyItems&quot;.&quot;Value&quot;
        ///	FROM &quot;LibraryHierarchyI [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetLibraryItemsWithFilter {
            get {
                return ResourceManager.GetString("GetLibraryItemsWithFilter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT &quot;MetaDataItems&quot;.*
        ///FROM &quot;MetaDataItems&quot;
        ///	JOIN &quot;LibraryItem_MetaDataItem&quot; ON &quot;LibraryItem_MetaDataItem&quot;.&quot;MetaDataItem_Id&quot; = &quot;MetaDataItems&quot;.&quot;Id&quot;
        ///WHERE &quot;LibraryItem_MetaDataItem&quot;.&quot;LibraryItem_Id&quot; = @libraryItemId 
        ///	AND (@type &amp; &quot;MetaDataItems&quot;.&quot;Type&quot;) =  &quot;MetaDataItems&quot;.&quot;Type&quot;.
        /// </summary>
        internal static string GetLibraryMetaData {
            get {
                return ResourceManager.GetString("GetLibraryMetaData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO &quot;MetaDataItems&quot; (&quot;Name&quot;, &quot;Type&quot;, &quot;Value&quot;) 
        ///SELECT @name, @type, @value
        ///WHERE NOT EXISTS(
        ///	SELECT *
        ///	FROM &quot;MetaDataItems&quot; 
        ///	WHERE &quot;Name&quot; = @name AND &quot;Type&quot; = @type AND &quot;Value&quot; = @value
        ///);
        ///
        ///SELECT &quot;Id&quot;
        ///FROM &quot;MetaDataItems&quot; 
        ///WHERE &quot;Name&quot; = @name AND &quot;Type&quot; = @type AND &quot;Value&quot; = @value;.
        /// </summary>
        internal static string GetOrAddMetaDataItem {
            get {
                return ResourceManager.GetString("GetOrAddMetaDataItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to WITH &quot;CurrentSequence&quot;
        ///AS
        ///(	
        ///		SELECT &quot;Sequence&quot; 
        ///		FROM &quot;PlaylistItems&quot;
        ///		WHERE &quot;Id&quot; = @id
        ///)
        ///
        ///UPDATE &quot;PlaylistItems&quot;
        ///SET &quot;Sequence&quot; = 
        ///(
        ///	CASE WHEN (SELECT &quot;Sequence&quot; FROM &quot;CurrentSequence&quot;) &lt; @sequence THEN
        ///		&quot;Sequence&quot; - 1
        ///	ELSE
        ///		&quot;Sequence&quot; + 1
        ///	END
        ///)
        ///WHERE 
        ///(
        ///	(SELECT &quot;Sequence&quot; FROM &quot;CurrentSequence&quot;) &lt; @sequence 
        ///		AND &quot;Sequence&quot; BETWEEN (SELECT &quot;Sequence&quot; FROM &quot;CurrentSequence&quot;) 
        ///			AND @sequence
        ///) 
        ///OR 
        ///(
        ///	&quot;Sequence&quot; BETWEEN @sequence 
        ///		AND (SELECT &quot;Sequence&quot; FROM &quot;Current [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string MovePlaylistItem {
            get {
                return ResourceManager.GetString("MovePlaylistItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE FROM [LibraryHierarchyItem_LibraryItem]
        ///WHERE [LibraryItem_Id] IN
        ///(
        ///	SELECT [Id] 
        ///	FROM [LibraryItems]
        ///	WHERE @status IS NULL OR [Status] = @status
        ///);
        ///
        ///DELETE FROM [LibraryHierarchyItems]
        ///WHERE [Id] IN
        ///(
        ///	SELECT [LibraryHierarchyItems].[Id]
        ///	FROM [LibraryHierarchyItems]
        ///		 LEFT JOIN [LibraryHierarchyItem_LibraryItem]
        ///			ON [LibraryHierarchyItems].[Id] = [LibraryHierarchyItem_LibraryItem].[LibraryHierarchyItem_Id]
        ///	WHERE [LibraryHierarchyItem_LibraryItem].[Id] IS NULL
        ///);.
        /// </summary>
        internal static string RemoveLibraryHierarchyItems {
            get {
                return ResourceManager.GetString("RemoveLibraryHierarchyItems", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE FROM [LibraryItem_MetaDataItem]
        ///WHERE [LibraryItem_Id] IN
        ///(
        ///	SELECT [Id]
        ///	FROM [LibraryItems]
        ///	WHERE @status IS NULL OR [LibraryItems].[Status] = @status
        ///);
        ///
        ///DELETE FROM [PlaylistItem_MetaDataItem]
        ///WHERE [PlaylistItem_Id] IN
        ///(
        ///	SELECT [PlaylistItems].[Id]
        ///	FROM [PlaylistItems]
        ///		JOIN [LibraryItems] ON [PlaylistItems].[LibraryItem_Id] = [LibraryItems].[Id]
        ///	WHERE @status IS NULL OR [LibraryItems].[Status] = @status
        ///);
        ///
        ///DELETE FROM [PlaylistItems]
        ///WHERE [Id] IN
        ///(
        ///	SELECT [PlaylistIt [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string RemoveLibraryItems {
            get {
                return ResourceManager.GetString("RemoveLibraryItems", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE FROM [PlaylistItem_MetaDataItem]
        ///WHERE [PlaylistItem_Id] IN
        ///(
        ///	SELECT [Id]
        ///	FROM [PlaylistItems]
        ///	WHERE [Status] = @status
        ///);
        ///
        ///DELETE FROM [PlaylistItems]
        ///WHERE [Status] = @status.
        /// </summary>
        internal static string RemovePlaylistItems {
            get {
                return ResourceManager.GetString("RemovePlaylistItems", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE &quot;LibraryItems&quot;
        ///SET &quot;Favorite&quot; = @isFavorite
        ///WHERE &quot;Id&quot; IN
        ///(
        ///	SELECT &quot;LibraryHierarchyItem_LibraryItem&quot;.&quot;LibraryItem_Id&quot;
        ///	FROM &quot;LibraryHierarchyItem_LibraryItem&quot;
        ///	WHERE &quot;LibraryHierarchyItem_LibraryItem&quot;.&quot;LibraryHierarchyItem_Id&quot; = @libraryHierarchyItemId
        ///).
        /// </summary>
        internal static string SetIsFavorite {
            get {
                return ResourceManager.GetString("SetIsFavorite", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO [LibraryHierarchyItem_LibraryItem] ([LibraryHierarchyItem_Id], [LibraryItem_Id])
        ///SELECT @libraryHierarchyItemId, @libraryItemId
        ///WHERE NOT EXISTS(
        ///	SELECT * 
        ///	FROM [LibraryHierarchyItem_LibraryItem]
        ///	WHERE [LibraryHierarchyItem_Id] = @libraryHierarchyItemId
        ///		AND [LibraryItem_Id] = @libraryItemId
        ///);.
        /// </summary>
        internal static string UpdateLibraryHierarchyNode {
            get {
                return ResourceManager.GetString("UpdateLibraryHierarchyNode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO &quot;MetaDataItems&quot; (&quot;Name&quot;, &quot;Type&quot;, &quot;Value&quot;)
        ///SELECT @name, @type, @value
        ///WHERE NOT EXISTS(SELECT * FROM &quot;MetaDataItems&quot; WHERE &quot;Name&quot; = @name AND &quot;Type&quot; = @type AND &quot;Value&quot; = @value);
        ///
        ///WITH &quot;MetaData&quot;
        ///AS
        ///(
        ///	SELECT 
        ///		&quot;LibraryItem_MetaDataItem&quot;.&quot;LibraryItem_Id&quot; AS &quot;Id&quot;,
        ///		&quot;MetaDataItems&quot;.&quot;Name&quot;,
        ///		&quot;MetaDataItems&quot;.&quot;Value&quot;
        ///	FROM &quot;LibraryItem_MetaDataItem&quot; 
        ///		JOIN &quot;MetaDataItems&quot; 
        ///			ON &quot;MetaDataItems&quot;.&quot;Id&quot; = &quot;LibraryItem_MetaDataItem&quot;.&quot;MetaDataItem_Id&quot;
        ///),
        ///
        ///&quot;Artist&quot;
        ///AS
        ///(
        ///	SELECT &quot;Arti [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string UpdateLibraryVariousArtists {
            get {
                return ResourceManager.GetString("UpdateLibraryVariousArtists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO &quot;MetaDataItems&quot; (&quot;Name&quot;, &quot;Type&quot;, &quot;Value&quot;)
        ///SELECT @name, @type, @value
        ///WHERE NOT EXISTS(SELECT * FROM &quot;MetaDataItems&quot; WHERE &quot;Name&quot; = @name AND &quot;Type&quot; = @type AND &quot;Value&quot; = @value);
        ///
        ///WITH &quot;MetaData&quot;
        ///AS
        ///(
        ///	SELECT 
        ///		&quot;PlaylistItem_MetaDataItem&quot;.&quot;PlaylistItem_Id&quot; AS &quot;Id&quot;,
        ///		&quot;MetaDataItems&quot;.&quot;Name&quot;,
        ///		&quot;MetaDataItems&quot;.&quot;Value&quot;
        ///	FROM &quot;PlaylistItem_MetaDataItem&quot; 
        ///		JOIN &quot;MetaDataItems&quot; 
        ///			ON &quot;MetaDataItems&quot;.&quot;Id&quot; = &quot;PlaylistItem_MetaDataItem&quot;.&quot;MetaDataItem_Id&quot;
        ///),
        ///
        ///&quot;Artist&quot;
        ///AS
        ///(
        ///	SELECT &quot; [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string UpdatePlaylistVariousArtists {
            get {
                return ResourceManager.GetString("UpdatePlaylistVariousArtists", resourceCulture);
            }
        }
    }
}
