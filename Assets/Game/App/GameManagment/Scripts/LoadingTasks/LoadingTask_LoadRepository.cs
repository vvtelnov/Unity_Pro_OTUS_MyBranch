using System;
using Services;

namespace Game.App
{
    public sealed class LoadingTask_LoadRepository : ILoadingTask
    {
        private readonly GameRepository repository;

        [ServiceInject]
        public LoadingTask_LoadRepository(GameRepository repository)
        {
            this.repository = repository;
        }

        async void ILoadingTask.Do(Action<LoadingResult> callback)
        {
            await this.repository.LoadState();
            callback?.Invoke(LoadingResult.Success());
        }
    }
}