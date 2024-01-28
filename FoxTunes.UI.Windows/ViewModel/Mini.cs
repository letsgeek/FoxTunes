﻿using FoxTunes.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FoxTunes.ViewModel
{
    public class Mini : ViewModelBase
    {
        public IPlaylistBrowser PlaylistBrowser { get; private set; }

        public IPlaylistManager PlaylistManager { get; private set; }

        public IConfiguration Configuration { get; private set; }

        public Icon Icon
        {
            get
            {
                using (var stream = typeof(Mini).Assembly.GetManifestResourceStream("FoxTunes.UI.Windows.Images.Fox.ico"))
                {
                    if (stream == null)
                    {
                        return null;
                    }
                    return new Icon(stream);
                }
            }
        }

        private BooleanConfigurationElement _ShowArtwork { get; set; }

        public BooleanConfigurationElement ShowArtwork
        {
            get
            {
                return this._ShowArtwork;
            }
            set
            {
                this._ShowArtwork = value;
                this.OnShowArtworkChanged();
            }
        }

        protected virtual void OnShowArtworkChanged()
        {
            if (this.ShowArtworkChanged != null)
            {
                this.ShowArtworkChanged(this, EventArgs.Empty);
            }
            this.OnPropertyChanged("ShowArtwork");
        }

        public event EventHandler ShowArtworkChanged;

        private BooleanConfigurationElement _ShowPlaylist { get; set; }

        public BooleanConfigurationElement ShowPlaylist
        {
            get
            {
                return this._ShowPlaylist;
            }
            set
            {
                this._ShowPlaylist = value;
                this.OnShowPlaylistChanged();
            }
        }

        protected virtual void OnShowPlaylistChanged()
        {
            if (this.ShowPlaylistChanged != null)
            {
                this.ShowPlaylistChanged(this, EventArgs.Empty);
            }
            this.OnPropertyChanged("ShowPlaylist");
        }

        public event EventHandler ShowPlaylistChanged;

        private SelectionConfigurationElement _DropCommit { get; set; }

        public SelectionConfigurationElement DropCommit
        {
            get
            {
                return this._DropCommit;
            }
            set
            {
                this._DropCommit = value;
                this.OnDropCommitChanged();
            }
        }

        protected virtual void OnDropCommitChanged()
        {
            if (this.DropCommitChanged != null)
            {
                this.DropCommitChanged(this, EventArgs.Empty);
            }
            this.OnPropertyChanged("DropCommit");
        }

        public event EventHandler DropCommitChanged;

        private BooleanConfigurationElement _ShowNotifyIcon { get; set; }

        public BooleanConfigurationElement ShowNotifyIcon
        {
            get
            {
                return this._ShowNotifyIcon;
            }
            set
            {
                this._ShowNotifyIcon = value;
                this.OnShowNotifyIconChanged();
            }
        }

        protected virtual void OnShowNotifyIconChanged()
        {
            if (this.ShowNotifyIconChanged != null)
            {
                this.ShowNotifyIconChanged(this, EventArgs.Empty);
            }
            this.OnPropertyChanged("ShowNotifyIcon");
        }

        public event EventHandler ShowNotifyIconChanged;

        private DoubleConfigurationElement _ScalingFactor { get; set; }

        public DoubleConfigurationElement ScalingFactor
        {
            get
            {
                return this._ScalingFactor;
            }
            set
            {
                this._ScalingFactor = value;
                this.OnScalingFactorChanged();
            }
        }

        protected virtual void OnScalingFactorChanged()
        {
            if (this.ScalingFactorChanged != null)
            {
                this.ScalingFactorChanged(this, EventArgs.Empty);
            }
            this.OnPropertyChanged("ScalingFactor");
        }

        public event EventHandler ScalingFactorChanged;

        private BooleanConfigurationElement _ExtendGlass { get; set; }

        public BooleanConfigurationElement ExtendGlass
        {
            get
            {
                return this._ExtendGlass;
            }
            set
            {
                this._ExtendGlass = value;
                this.OnExtendGlassChanged();
            }
        }

        protected virtual void OnExtendGlassChanged()
        {
            if (this.ExtendGlassChanged != null)
            {
                this.ExtendGlassChanged(this, EventArgs.Empty);
            }
            this.OnPropertyChanged("ExtendGlass");
        }

        public event EventHandler ExtendGlassChanged;

        public ICommand DragEnterCommand
        {
            get
            {
                return new Command<DragEventArgs>(this.OnDragEnter);
            }
        }

        protected virtual void OnDragEnter(DragEventArgs e)
        {
            this.UpdateDragDropEffects(e);
        }

        public ICommand DragOverCommand
        {
            get
            {
                return new Command<DragEventArgs>(this.OnDragOver);
            }
        }

        protected virtual void OnDragOver(DragEventArgs e)
        {
            this.UpdateDragDropEffects(e);
        }

        protected virtual void UpdateDragDropEffects(DragEventArgs e)
        {
            var effects = DragDropEffects.None;
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    effects = DragDropEffects.Copy;
                }
#if VISTA
                if (ShellIDListHelper.GetDataPresent(e.Data))
                {
                    effects = DragDropEffects.Copy;
                }
#endif
            }
            catch (Exception exception)
            {
                Logger.Write(this, LogLevel.Warn, "Failed to query clipboard contents: {0}", exception.Message);
            }
            e.Effects = effects;
        }

        public ICommand DropCommand
        {
            get
            {
                return CommandFactory.Instance.CreateCommand<DragEventArgs>(
                    new Func<DragEventArgs, Task>(this.OnDrop)
                );
            }
        }

        protected virtual Task OnDrop(DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    var paths = e.Data.GetData(DataFormats.FileDrop) as IEnumerable<string>;
                    return this.AddToPlaylist(paths);
                }
#if VISTA
                if (ShellIDListHelper.GetDataPresent(e.Data))
                {
                    var paths = ShellIDListHelper.GetData(e.Data);
                    return this.AddToPlaylist(paths);
                }
#endif
            }
            catch (Exception exception)
            {
                Logger.Write(this, LogLevel.Warn, "Failed to process clipboard contents: {0}", exception.Message);
            }
#if NET40
            return TaskEx.FromResult(false);
#else
            return Task.CompletedTask;
#endif
        }

        private async Task AddToPlaylist(IEnumerable<string> paths)
        {
            var clear = default(bool);
            var play = default(bool);
            switch (MiniPlayerBehaviourConfiguration.GetDropBehaviour(this.DropCommit.Value))
            {
                case MiniPlayerDropBehaviour.Append:
                    clear = false;
                    play = false;
                    break;
                case MiniPlayerDropBehaviour.AppendAndPlay:
                    clear = false;
                    play = true;
                    break;
                case MiniPlayerDropBehaviour.Replace:
                    clear = true;
                    play = false;
                    break;
                case MiniPlayerDropBehaviour.ReplaceAndPlay:
                    clear = true;
                    play = true;
                    break;
            }
            var index = await this.PlaylistBrowser.GetInsertIndex().ConfigureAwait(false);
            await this.PlaylistManager.Add(paths, clear).ConfigureAwait(false);
            if (play)
            {
                await this.PlaylistManager.Play(index).ConfigureAwait(false);
            }
        }

        public override void InitializeComponent(ICore core)
        {
            this.PlaylistBrowser = this.Core.Components.PlaylistBrowser;
            this.PlaylistManager = this.Core.Managers.Playlist;
            this.Configuration = this.Core.Components.Configuration;
            this.ShowArtwork = this.Configuration.GetElement<BooleanConfigurationElement>(
              MiniPlayerBehaviourConfiguration.SECTION,
              MiniPlayerBehaviourConfiguration.SHOW_ARTWORK_ELEMENT
            );
            this.ShowPlaylist = this.Configuration.GetElement<BooleanConfigurationElement>(
              MiniPlayerBehaviourConfiguration.SECTION,
              MiniPlayerBehaviourConfiguration.SHOW_PLAYLIST_ELEMENT
            );
            this.DropCommit = this.Configuration.GetElement<SelectionConfigurationElement>(
              MiniPlayerBehaviourConfiguration.SECTION,
              MiniPlayerBehaviourConfiguration.DROP_COMMIT_ELEMENT
            );
            this.ShowNotifyIcon = this.Configuration.GetElement<BooleanConfigurationElement>(
              NotifyIconConfiguration.SECTION,
              NotifyIconConfiguration.ENABLED_ELEMENT
            );
            this.ScalingFactor = this.Configuration.GetElement<DoubleConfigurationElement>(
              WindowsUserInterfaceConfiguration.SECTION,
              WindowsUserInterfaceConfiguration.UI_SCALING_ELEMENT
            );
            this.ExtendGlass = this.Configuration.GetElement<BooleanConfigurationElement>(
              WindowsUserInterfaceConfiguration.SECTION,
              WindowsUserInterfaceConfiguration.EXTEND_GLASS_ELEMENT
            );
            base.InitializeComponent(core);
        }

        public ICommand RestoreCommand
        {
            get
            {
                return new Command(this.Restore);
            }
        }

        public void Restore()
        {
            if (Windows.IsMiniWindowCreated && Windows.ActiveWindow == Windows.MiniWindow)
            {
                Windows.ActiveWindow.Show();
                if (Windows.ActiveWindow.WindowState == WindowState.Minimized)
                {
                    Windows.ActiveWindow.WindowState = WindowState.Normal;
                }
                Windows.ActiveWindow.BringToFront();
            }
        }

        protected override Freezable CreateInstanceCore()
        {
            return new Mini();
        }
    }
}
