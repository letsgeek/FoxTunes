﻿SELECT "MetaDataItems"."Name", "MetaDataItems"."Type", "MetaDataItems"."NumericValue",  "MetaDataItems"."TextValue", "MetaDataItems"."FileValue"
FROM "LibraryHierarchyItems"
	JOIN "LibraryHierarchyItem_LibraryItem" ON "LibraryHierarchyItems"."Id" = "LibraryHierarchyItem_LibraryItem"."LibraryHierarchyItem_Id"
	JOIN "LibraryItem_MetaDataItem" ON "LibraryHierarchyItem_LibraryItem"."LibraryItem_Id" = "LibraryItem_MetaDataItem"."LibraryItem_Id"
	JOIN "MetaDataItems" ON "MetaDataItems"."Id" = "LibraryItem_MetaDataItem"."MetaDataItem_Id"
WHERE "LibraryHierarchyItems"."Id" = @libraryHierarchyItemId 
	AND "MetaDataItems"."Type" = @type
GROUP BY "MetaDataItems"."Name", "MetaDataItems"."Type", "MetaDataItems"."NumericValue",  "MetaDataItems"."TextValue", "MetaDataItems"."FileValue"