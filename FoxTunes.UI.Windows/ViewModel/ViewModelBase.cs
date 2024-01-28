﻿using FoxTunes.Interfaces;
using System;
using System.ComponentModel;
using System.Windows;

namespace FoxTunes.ViewModel
{
    public abstract class ViewModelBase : Freezable, INotifyPropertyChanging, INotifyPropertyChanged
    {
        public static readonly DependencyProperty CoreProperty = DependencyProperty.Register("Core", typeof(ICore), typeof(ViewModelBase), new PropertyMetadata(new PropertyChangedCallback(OnCoreChanged)));

        public static void OnCoreChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var viewModel = sender as ViewModelBase;
            viewModel.OnCoreChanged();
        }

        public ICore Core
        {
            get
            {
                return this.GetValue(CoreProperty) as ICore;
            }
            set
            {
                this.SetValue(CoreProperty, value);
            }
        }

        protected virtual void OnCoreChanged()
        {
            if (this.CoreChanged != null)
            {
                this.CoreChanged(this, EventArgs.Empty);
            }
            this.OnPropertyChanged("Core");
        }

        public event EventHandler CoreChanged = delegate { };

        protected virtual void OnPropertyChanging(string propertyName)
        {
            if (this.PropertyChanged == null)
            {
                return;
            }
            this.PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
        }

        public event PropertyChangingEventHandler PropertyChanging = delegate { };

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged == null)
            {
                return;
            }
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
