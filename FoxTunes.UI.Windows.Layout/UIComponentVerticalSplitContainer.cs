﻿using FoxTunes.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace FoxTunes
{
    /// <summary>
    /// Interaction logic for UIComponentVerticalSplitContainer.xaml
    /// </summary>
    [UIComponent("18E98420-F039-4504-A116-3D0F26BEAAD5", UIComponentSlots.NONE, "Vertical Split", role: UIComponentRole.Hidden)]
    public partial class UIComponentVerticalSplitContainer : UIComponentPanel
    {
        const string COLLAPSE_LEFT = "AAAA";

        const string COLLAPSE_RIGHT = "BBBB";

        public static readonly DependencyProperty LeftComponentProperty = DependencyProperty.Register(
            "LeftComponent",
            typeof(UIComponentConfiguration),
            typeof(UIComponentVerticalSplitContainer),
            new PropertyMetadata(new PropertyChangedCallback(OnLeftComponentChanged))
        );

        public static UIComponentConfiguration GetLeftComponent(UIComponentVerticalSplitContainer source)
        {
            return (UIComponentConfiguration)source.GetValue(LeftComponentProperty);
        }

        public static void SetLeftComponent(UIComponentVerticalSplitContainer source, UIComponentConfiguration value)
        {
            source.SetValue(LeftComponentProperty, value);
        }

        public static void OnLeftComponentChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var container = sender as UIComponentVerticalSplitContainer;
            if (container == null)
            {
                return;
            }
            container.OnLeftComponentChanged();
        }

        public static readonly DependencyProperty RightComponentProperty = DependencyProperty.Register(
            "RightComponent",
            typeof(UIComponentConfiguration),
            typeof(UIComponentVerticalSplitContainer),
            new PropertyMetadata(new PropertyChangedCallback(OnRightComponentChanged))
        );

        public static UIComponentConfiguration GetRightComponent(UIComponentVerticalSplitContainer source)
        {
            return (UIComponentConfiguration)source.GetValue(RightComponentProperty);
        }

        public static void SetRightComponent(UIComponentVerticalSplitContainer source, UIComponentConfiguration value)
        {
            source.SetValue(RightComponentProperty, value);
        }

        public static void OnRightComponentChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var container = sender as UIComponentVerticalSplitContainer;
            if (container == null)
            {
                return;
            }
            container.OnRightComponentChanged();
        }

        public static readonly DependencyProperty LeftEnabledProperty = DependencyProperty.Register(
            "LeftEnabled",
            typeof(bool),
            typeof(UIComponentVerticalSplitContainer),
            new PropertyMetadata(true, new PropertyChangedCallback(OnLeftEnabledChanged))
        );

        public static bool GetLeftEnabled(UIComponentVerticalSplitContainer source)
        {
            return (bool)source.GetValue(LeftEnabledProperty);
        }

        public static void SetLeftEnabled(UIComponentVerticalSplitContainer source, bool value)
        {
            source.SetValue(LeftEnabledProperty, value);
        }

        public static void OnLeftEnabledChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var container = sender as UIComponentVerticalSplitContainer;
            if (container == null)
            {
                return;
            }
            container.OnLeftEnabledChanged();
        }

        public static readonly DependencyProperty RightEnabledProperty = DependencyProperty.Register(
            "RightEnabled",
            typeof(bool),
            typeof(UIComponentVerticalSplitContainer),
            new PropertyMetadata(true, new PropertyChangedCallback(OnRightEnabledChanged))
        );

        public static bool GetRightEnabled(UIComponentVerticalSplitContainer source)
        {
            return (bool)source.GetValue(RightEnabledProperty);
        }

        public static void SetRightEnabled(UIComponentVerticalSplitContainer source, bool value)
        {
            source.SetValue(RightEnabledProperty, value);
        }

        public static void OnRightEnabledChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var container = sender as UIComponentVerticalSplitContainer;
            if (container == null)
            {
                return;
            }
            container.OnRightEnabledChanged();
        }

        public static readonly DependencyProperty SplitterDistanceProperty = DependencyProperty.Register(
            "SplitterDistance",
            typeof(string),
            typeof(UIComponentVerticalSplitContainer),
            new PropertyMetadata(new PropertyChangedCallback(OnSplitterDistanceChanged))
        );

        public static string GetSplitterDistance(UIComponentVerticalSplitContainer source)
        {
            return (string)source.GetValue(SplitterDistanceProperty);
        }

        public static void SetSplitterDistance(UIComponentVerticalSplitContainer source, string value)
        {
            source.SetValue(SplitterDistanceProperty, value);
        }

        public static void OnSplitterDistanceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var container = sender as UIComponentVerticalSplitContainer;
            if (container == null)
            {
                return;
            }
            container.OnSplitterDistanceChanged();
        }

        public static readonly DependencyProperty CollapseLeftProperty = DependencyProperty.Register(
            "CollapseLeft",
            typeof(bool),
            typeof(UIComponentVerticalSplitContainer),
            new PropertyMetadata(new PropertyChangedCallback(OnCollapseLeftChanged))
        );

        public static bool GetCollapseLeft(UIComponentVerticalSplitContainer source)
        {
            return (bool)source.GetValue(CollapseLeftProperty);
        }

        public static void SetCollapseLeft(UIComponentVerticalSplitContainer source, bool value)
        {
            source.SetValue(CollapseLeftProperty, value);
        }

        public static void OnCollapseLeftChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var container = sender as UIComponentVerticalSplitContainer;
            if (container == null)
            {
                return;
            }
            container.OnCollapseLeftChanged();
        }

        public static readonly DependencyProperty CollapseRightProperty = DependencyProperty.Register(
            "CollapseRight",
            typeof(bool),
            typeof(UIComponentVerticalSplitContainer),
            new PropertyMetadata(new PropertyChangedCallback(OnCollapseRightChanged))
        );

        public static bool GetCollapseRight(UIComponentVerticalSplitContainer source)
        {
            return (bool)source.GetValue(CollapseRightProperty);
        }

        public static void SetCollapseRight(UIComponentVerticalSplitContainer source, bool value)
        {
            source.SetValue(CollapseRightProperty, value);
        }

        public static void OnCollapseRightChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var container = sender as UIComponentVerticalSplitContainer;
            if (container == null)
            {
                return;
            }
            container.OnCollapseRightChanged();
        }

        public UIComponentVerticalSplitContainer()
        {
            this.InitializeComponent();
        }

        public UIComponentContainer LeftContainer { get; private set; }

        public UIComponentContainer RightContainer { get; private set; }

        private void InitializeComponent()
        {
            this.LeftContainer = new UIComponentContainer();
            this.RightContainer = new UIComponentContainer();

            Grid.SetColumn(this.LeftContainer, 0);
            Grid.SetColumn(this.RightContainer, 2);

            this.LeftContainer.SetBinding(
                UIComponentContainer.ComponentProperty,
                new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(this.LeftComponent))
                }
            );
            this.RightContainer.SetBinding(
                UIComponentContainer.ComponentProperty,
                new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(this.RightComponent))
                }
            );

            this.SetBinding(
                UIComponentVerticalSplitContainer.LeftEnabledProperty,
                new Binding()
                {
                    Source = this.LeftContainer,
                    Path = new PropertyPath("Content.IsComponentEnabled"),
                    FallbackValue = true
                }
            );
            this.SetBinding(
                UIComponentVerticalSplitContainer.RightEnabledProperty,
                new Binding()
                {
                    Source = this.RightContainer,
                    Path = new PropertyPath("Content.IsComponentEnabled"),
                    FallbackValue = true
                }
            );
        }

        protected virtual void CreateLayout()
        {
            this.LeftContainer.Disconnect();
            this.RightContainer.Disconnect();
            if (this.IsInDesignMode)
            {
                this.CreateSplitLayout();
                this.IsComponentEnabled = true;
            }
            else if (this.LeftEnabled && this.RightEnabled)
            {
                this.CreateSplitLayout();
                this.IsComponentEnabled = true;
            }
            else if (this.LeftEnabled)
            {
                this.CreateLayout(this.LeftContainer);
                this.IsComponentEnabled = true;
            }
            else if (this.RightEnabled)
            {
                this.CreateLayout(this.RightContainer);
                this.IsComponentEnabled = true;
            }
            else
            {
                this.IsComponentEnabled = false;
            }
        }

        protected virtual void CreateSplitLayout()
        {
            var grid = new Grid();

            var topColumn = new ColumnDefinition();
            var splitterColumn = new ColumnDefinition();
            var bottomColumn = new ColumnDefinition();

            var splitter = new GridSplitter();

            topColumn.SetBinding(
                ColumnDefinition.WidthProperty,
                new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(this.SplitterDistance)),
                    Converter = new global::FoxTunes.ViewModel.GridLengthConverter(),
                    Mode = BindingMode.TwoWay
                }
            );

            splitterColumn.Width = new GridLength(0, GridUnitType.Auto);

            Grid.SetColumn(splitter, 1);
            splitter.Width = 4;
            splitter.HorizontalAlignment = HorizontalAlignment.Center;
            splitter.VerticalAlignment = VerticalAlignment.Stretch;

            grid.ColumnDefinitions.Add(topColumn);
            grid.ColumnDefinitions.Add(splitterColumn);
            grid.ColumnDefinitions.Add(bottomColumn);

            grid.Children.Add(this.LeftContainer);
            grid.Children.Add(splitter);
            grid.Children.Add(this.RightContainer);

            this.Content = grid;
        }

        protected virtual void CreateLayout(UIComponentContainer container)
        {
            this.Content = container;
        }

        protected override void OnIsInDesignModeChanged()
        {
            this.CreateLayout();
            base.OnIsInDesignModeChanged();
        }

        protected override void OnComponentChanged()
        {
            if (this.Component != null)
            {
                this.UpdateChildren();
                this.UpdateSplitterDistance();
                this.CreateLayout();
            }
            base.OnComponentChanged();
        }

        protected virtual void UpdateChildren()
        {
            if (this.Component.Children != null && this.Component.Children.Count == 2)
            {
                this.LeftComponent = this.Component.Children[0];
                this.RightComponent = this.Component.Children[1];
            }
            else
            {
                this.Component.Children = new ObservableCollection<UIComponentConfiguration>()
                {
                    new UIComponentConfiguration(),
                    new UIComponentConfiguration()
                };
            }
        }

        protected virtual void UpdateSplitterDistance()
        {
            var splitterDistance = default(string);
            if (this.Component.TryGet(nameof(this.SplitterDistance), out splitterDistance))
            {
                this.SplitterDistance = splitterDistance;
            }
            else
            {
                this.SplitterDistance = "1*";
            }
        }

        public UIComponentConfiguration LeftComponent
        {
            get
            {
                return this.GetValue(LeftComponentProperty) as UIComponentConfiguration;
            }
            set
            {
                this.SetValue(LeftComponentProperty, value);
            }
        }

        protected virtual void OnLeftComponentChanged()
        {
            if (this.Component != null && this.Component.Children.Count == 2 && this.LeftComponent != null)
            {
                this.Component.Children[0] = this.LeftComponent;
            }
            if (this.LeftComponentChanged != null)
            {
                this.LeftComponentChanged(this, EventArgs.Empty);
            }
            this.OnPropertyChanged("LeftComponent");
        }

        public event EventHandler LeftComponentChanged;

        public UIComponentConfiguration RightComponent
        {
            get
            {
                return this.GetValue(RightComponentProperty) as UIComponentConfiguration;
            }
            set
            {
                this.SetValue(RightComponentProperty, value);
            }
        }

        protected virtual void OnRightComponentChanged()
        {
            if (this.Component != null && this.Component.Children.Count == 2 && this.RightComponent != null)
            {
                this.Component.Children[1] = this.RightComponent;
            }
            if (this.RightComponentChanged != null)
            {
                this.RightComponentChanged(this, EventArgs.Empty);
            }
            this.OnPropertyChanged("RightComponent");
        }

        public event EventHandler RightComponentChanged;

        public bool LeftEnabled
        {
            get
            {
                return (bool)this.GetValue(LeftEnabledProperty);
            }
            set
            {
                this.SetValue(LeftEnabledProperty, value);
            }
        }

        protected virtual void OnLeftEnabledChanged()
        {
            this.CreateLayout();
            if (this.LeftEnabledChanged != null)
            {
                this.LeftEnabledChanged(this, EventArgs.Empty);
            }
            this.OnPropertyChanged("LeftEnabled");
        }

        public event EventHandler LeftEnabledChanged;

        public bool RightEnabled
        {
            get
            {
                return (bool)this.GetValue(RightEnabledProperty);
            }
            set
            {
                this.SetValue(RightEnabledProperty, value);
            }
        }

        protected virtual void OnRightEnabledChanged()
        {
            this.CreateLayout();
            if (this.RightEnabledChanged != null)
            {
                this.RightEnabledChanged(this, EventArgs.Empty);
            }
            this.OnPropertyChanged("RightEnabled");
        }

        public event EventHandler RightEnabledChanged;

        public string SplitterDistance
        {
            get
            {
                return (string)this.GetValue(SplitterDistanceProperty);
            }
            set
            {
                this.SetValue(SplitterDistanceProperty, value);
            }
        }

        protected virtual void OnSplitterDistanceChanged()
        {
            if (this.Component != null)
            {
                this.Component.AddOrUpdate(
                    nameof(this.SplitterDistance),
                    this.SplitterDistance
                );
            }
            if (this.SplitterDistanceChanged != null)
            {
                this.SplitterDistanceChanged(this, EventArgs.Empty);
            }
            this.OnPropertyChanged("SplitterDistance");
        }

        public event EventHandler SplitterDistanceChanged;

        public bool CollapseLeft
        {
            get
            {
                return (bool)this.GetValue(CollapseLeftProperty);
            }
            set
            {
                this.SetValue(CollapseLeftProperty, value);
            }
        }

        protected virtual void OnCollapseLeftChanged()
        {
            if (this.Component != null)
            {
                this.Component.AddOrUpdate(
                    nameof(this.CollapseLeft),
                    Convert.ToString(this.CollapseLeft)
                );
            }
            if (this.CollapseLeftChanged != null)
            {
                this.CollapseLeftChanged(this, EventArgs.Empty);
            }
            this.OnPropertyChanged("CollapseLeft");
        }

        public event EventHandler CollapseLeftChanged;

        public bool CollapseRight
        {
            get
            {
                return (bool)this.GetValue(CollapseRightProperty);
            }
            set
            {
                this.SetValue(CollapseRightProperty, value);
            }
        }

        protected virtual void OnCollapseRightChanged()
        {
            if (this.Component != null)
            {
                this.Component.AddOrUpdate(
                    nameof(this.CollapseRight),
                    Convert.ToString(this.CollapseRight)
                );
            }
            if (this.CollapseRightChanged != null)
            {
                this.CollapseRightChanged(this, EventArgs.Empty);
            }
            this.OnPropertyChanged("CollapseRight");
        }

        public event EventHandler CollapseRightChanged;

        public override IEnumerable<IInvocationComponent> Invocations
        {
            get
            {
                yield return new InvocationComponent(
                    InvocationComponent.CATEGORY_GLOBAL,
                    COLLAPSE_LEFT,
                    "Collapsable Left", attributes:
                    this.CollapseLeft ? InvocationComponent.ATTRIBUTE_SELECTED : InvocationComponent.ATTRIBUTE_NONE
                );
                yield return new InvocationComponent(
                    InvocationComponent.CATEGORY_GLOBAL,
                    COLLAPSE_LEFT,
                    "Collapsable Right", attributes:
                    this.CollapseRight ? InvocationComponent.ATTRIBUTE_SELECTED : InvocationComponent.ATTRIBUTE_NONE
                );
            }
        }

        public override Task InvokeAsync(IInvocationComponent component)
        {
            switch (component.Id)
            {
                case COLLAPSE_LEFT:
                    return Windows.Invoke(() => this.CollapseLeft = !this.CollapseLeft);
                case COLLAPSE_RIGHT:
                    return Windows.Invoke(() => this.CollapseRight = !this.CollapseRight);
            }
            return base.InvokeAsync(component);
        }
    }
}
