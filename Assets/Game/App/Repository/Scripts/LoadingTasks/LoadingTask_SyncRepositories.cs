using System;
using Services;

namespace Game.App.LoadingTasks
{
    public sealed class LoadingTask_SyncRepositories : ILoadingTask
    {
        private readonly RepositorySynchronizer synchronizer;
        
        [ServiceInject]
        public LoadingTask_SyncRepositories(RepositorySynchronizer synchronizer)
        {
            this.synchronizer = synchronizer;
        }

        void ILoadingTask.Do(Action<LoadingResult> callback)
        {
            this.synchronizer.SyncRepositories();
            callback?.Invoke(LoadingResult.Success());
        }
    }
}