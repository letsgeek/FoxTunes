﻿using System.Collections.ObjectModel;

namespace FoxTunes
{
    public class LibraryHierarchyItem : PersistableComponent
    {
        public LibraryHierarchyItem()
        {
            this.Children = new ObservableCollection<LibraryHierarchyItem>();
            this.Items = new ObservableCollection<LibraryItem>();
        }

        public LibraryHierarchyItem(string displayValue, string sortValue, bool isLeaf) : this()
        {
            this.DisplayValue = displayValue;
            this.SortValue = sortValue;
            this.IsLeaf = isLeaf;
        }

        public string DisplayValue { get; set; }

        public string SortValue { get; set; }

        public LibraryHierarchyItem Parent { get; set; }

        public virtual ObservableCollection<LibraryHierarchyItem> Children { get; set; }

        public virtual ObservableCollection<LibraryItem> Items { get; set; }

        public bool IsLeaf { get; set; }
    }
}
