﻿using FoxTunes.Interfaces;
using System;
using System.Collections.ObjectModel;

namespace FoxTunes
{
    public class LibraryHierarchy : PersistableComponent, ISequenceableComponent
    {
        private int _Sequence { get; set; }

        public int Sequence
        {
            get
            {
                return this._Sequence;
            }
            set
            {
                this._Sequence = value;
                this.OnSequenceChanged();
            }
        }

        protected virtual void OnSequenceChanged()
        {
            if (this.SequenceChanged != null)
            {
                this.SequenceChanged(this, EventArgs.Empty);
            }
            this.OnPropertyChanged("Sequence");
        }

        public event EventHandler SequenceChanged;

        private string _Name { get; set; }

        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
                this.OnNameChanged();
            }
        }

        protected virtual void OnNameChanged()
        {
            if (this.NameChanged != null)
            {
                this.NameChanged(this, EventArgs.Empty);
            }
            this.OnPropertyChanged("Name");
        }

        public event EventHandler NameChanged;

        private LibraryHierarchyType _Type { get; set; }

        public LibraryHierarchyType Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
                this.OnTypeChanged();
            }
        }

        protected virtual void OnTypeChanged()
        {
            if (this.TypeChanged != null)
            {
                this.TypeChanged(this, EventArgs.Empty);
            }
            this.OnPropertyChanged("Type");
        }

        public event EventHandler TypeChanged;

        public ObservableCollection<LibraryHierarchyLevel> Levels { get; set; }

        public static readonly LibraryHierarchy Empty = new LibraryHierarchy();
    }

    public enum LibraryHierarchyType : byte
    {
        None = 0,
        Script = 1,
        FileSystem = 2
    }
}
