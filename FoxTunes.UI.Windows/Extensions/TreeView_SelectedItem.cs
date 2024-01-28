﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FoxTunes
{
    public static partial class TreeViewExtensions
    {
        private static readonly Dictionary<TreeView, SelectedItemBehaviour> SelectedItemBehaviours = new Dictionary<TreeView, SelectedItemBehaviour>();

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.RegisterAttached(
            "SelectedItem",
            typeof(object),
            typeof(TreeViewExtensions),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnSelectedItemPropertyChanged))
        );

        public static object GetSelectedItem(TreeView source)
        {
            return source.GetValue(SelectedItemProperty);
        }

        public static void SetSelectedItem(TreeView source, object value)
        {
            source.SetValue(SelectedItemProperty, value);
        }

        private static void OnSelectedItemPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var treeView = sender as TreeView;
            if (treeView == null)
            {
                return;
            }
            if (!SelectedItemBehaviours.ContainsKey(treeView))
            {
                SelectedItemBehaviours.Add(treeView, new SelectedItemBehaviour(treeView));
            }
        }

        private class SelectedItemBehaviour
        {
            public SelectedItemBehaviour(TreeView treeView)
            {
                this.TreeView = treeView;
                this.TreeView.SelectedItemChanged += this.TreeView_SelectedItemChanged;
            }

            public TreeView TreeView { get; private set; }

            protected virtual void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
            {
                SetSelectedItem(this.TreeView, this.TreeView.SelectedItem);
                if (GetSelectedItem(this.TreeView) != this.TreeView.SelectedItem)
                {
                    //TODO: Sometimes the value doesn't stick. Don't know why. Second attempt probably works.
                    SetSelectedItem(this.TreeView, this.TreeView.SelectedItem);
                }
            }
        }
    }
}
