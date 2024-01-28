CREATE TABLE [MetaDataItems](
	[Id] INTEGER PRIMARY KEY NOT NULL, 
	[Name] text NOT NULL, 
	[Type] bigint NOT NULL,
	[Value] text);

CREATE TABLE [LibraryItems] (
	[Id] INTEGER PRIMARY KEY NOT NULL, 
	[DirectoryName] text NOT NULL, 
	[FileName] text NOT NULL, 
	[Status] INTEGER NOT NULL);

CREATE TABLE [PlaylistItems](
    [Id] INTEGER PRIMARY KEY NOT NULL, 
	[LibraryItem_Id] INTEGER NULL REFERENCES LibraryItems([Id]),
    [Sequence] bigint NOT NULL, 
    [DirectoryName] text NOT NULL, 
    [FileName] text NOT NULL, 
    [Status] bigint NOT NULL);

CREATE TABLE [LibraryHierarchies] ( 
	[Id] INTEGER PRIMARY KEY NOT NULL, 
	[Sequence] INTEGER NOT NULL, 
	[Name] TEXT NOT NULL);

CREATE TABLE [LibraryHierarchyLevels] (
	[Id]	INTEGER PRIMARY KEY NOT NULL,
	[LibraryHierarchy_Id]  INTEGER NULL REFERENCES LibraryHierarchies([Id]),
	[Sequence] INTEGER NOT NULL,
	[Name] text NOT NULL,
	[Script]	TEXT NOT NULL);

CREATE TABLE [LibraryHierarchyItems](
    [Id] INTEGER PRIMARY KEY NOT NULL, 
	[Parent_Id] INTEGER NULL REFERENCES LibraryHierarchyItems([Id]),
    [LibraryHierarchy_Id] bigint NOT NULL REFERENCES LibraryHierarchies([Id]),
    [LibraryHierarchyLevel_Id] bigint NOT NULL REFERENCES LibraryHierarchyLevels([Id]),
    [Value] text NOT NULL, 
    [IsLeaf] bit NOT NULL);

CREATE TABLE [PlaylistItem_MetaDataItem](
    [Id] INTEGER PRIMARY KEY NOT NULL, 
    [PlaylistItem_Id] INTEGER NOT NULL REFERENCES PlaylistItems([Id]),
    [MetaDataItem_Id] INTEGER NOT NULL REFERENCES MetaDataItems([Id]));

CREATE TABLE [PlaylistColumns] ( 
  [Id] INTEGER PRIMARY KEY NOT NULL, 
  [Sequence] INTEGER NOT NULL, 
  [Name] text NOT NULL, 
  [Script] text NOT NULL,
  [IsDynamic] INTEGER NOT NULL, 
  [Width] numeric(53,0) NULL);

CREATE TABLE [LibraryItem_MetaDataItem](
    [Id] INTEGER PRIMARY KEY NOT NULL, 
    [LibraryItem_Id] INTEGER NOT NULL REFERENCES LibraryItems([Id]),
    [MetaDataItem_Id] INTEGER NOT NULL REFERENCES MetaDataItems([Id]));

CREATE TABLE [LibraryHierarchyItem_LibraryItem] (
	[Id]	INTEGER PRIMARY KEY NOT NULL,
	[LibraryHierarchyItem_Id] INTEGER NOT NULL REFERENCES LibraryHierarchyItems([Id]),
	[LibraryItem_Id] INTEGER NOT NULL REFERENCES LibraryItems([Id]) 
);

CREATE INDEX [IDX_PlaylistItem_MetaDataItem_PlaylistItem_Id] ON [PlaylistItem_MetaDataItem](
	[PlaylistItem_Id]
);

CREATE INDEX [IDX_LibraryItem_MetaDataItem_LibraryItem_Id] ON [LibraryItem_MetaDataItem](
	[LibraryItem_Id]
);

CREATE INDEX [IDX_LibraryHierarchyItems_LibraryHierarchy_Id)] ON [LibraryHierarchyItems](
	[LibraryHierarchy_Id]
);