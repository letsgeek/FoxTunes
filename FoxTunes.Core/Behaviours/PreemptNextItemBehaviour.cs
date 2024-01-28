﻿using FoxTunes.Interfaces;
using System;
using System.Threading.Tasks;

namespace FoxTunes
{
    public class PreemptNextItemBehaviour : StandardBehaviour
    {
        public IOutput Output { get; private set; }

        public IPlaylistManager PlaylistManager { get; private set; }

        public IPlaybackManager PlaybackManager { get; private set; }

        public IOutputStreamQueue OutputStreamQueue { get; private set; }

        public IBackgroundTaskRunner BackgroundTaskRunner { get; private set; }

        public override void InitializeComponent(ICore core)
        {
            this.Output = core.Components.Output;
            this.PlaylistManager = core.Managers.Playlist;
            this.PlaybackManager = core.Managers.Playback;
            this.OutputStreamQueue = core.Components.OutputStreamQueue;
            this.PlaybackManager.CurrentStreamChanged += this.PlaybackManager_CurrentStreamChanged;
            this.BackgroundTaskRunner = core.Components.BackgroundTaskRunner;
            base.InitializeComponent(core);
        }

        protected virtual void PlaybackManager_CurrentStreamChanged(object sender, EventArgs e)
        {
            if (this.PlaybackManager.CurrentStream == null)
            {
                return;
            }
            this.PlaybackManager.CurrentStream.Stopping += this.PlaybackManager_CurrentStream_Stopping;
        }

        protected virtual async void PlaybackManager_CurrentStream_Stopping(object sender, AsyncEventArgs e)
        {
            using (e.Defer())
            {
                await this.Preempt();
            }
        }

        public async Task Preempt()
        {
            var playlistItem = await this.PlaylistManager.GetNext();
            if (playlistItem == null)
            {
                return;
            }
            var outputStream = this.OutputStreamQueue.Peek(playlistItem);
            if (outputStream == null)
            {
                return;
            }
            Logger.Write(this, LogLevel.Debug, "Current stream is about to end, pre-empting the next stream: {0} => {1}", outputStream.Id, outputStream.FileName);
            await this.BackgroundTaskRunner.Run(async () =>
            {
                if (!await this.Output.Preempt(outputStream))
                {
                    Logger.Write(this, LogLevel.Debug, "Pre-empt failed for stream: {0} => {1}", outputStream.Id, outputStream.FileName);
                }
            });
        }
    }
}
