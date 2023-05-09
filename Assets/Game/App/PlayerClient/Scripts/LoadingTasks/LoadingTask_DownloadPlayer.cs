using System;
using Services;

namespace Game.App.LoadingTasks
{
    public sealed class LoadingTask_DownloadPlayer : ILoadingTask
    {
        private readonly PlayerDownloader playerDownloader;

        [ServiceInject]
        public LoadingTask_DownloadPlayer(PlayerDownloader playerDownloader)
        {
            this.playerDownloader = playerDownloader;
        }

        void ILoadingTask.Do(Action<LoadingResult> callback)
        {
            this.playerDownloader.DownloadState(_ => callback.Invoke(LoadingResult.Success()));
        }
    }
}