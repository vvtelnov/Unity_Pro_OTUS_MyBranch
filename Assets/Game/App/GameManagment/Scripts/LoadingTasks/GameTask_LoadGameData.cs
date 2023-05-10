using System;
using Services;

namespace Game.App
{
    public sealed class GameTask_LoadGameData : ILoadingTask
    {
        private readonly GameRepository repository;

        private readonly IGameMediator[] mediators;

        [ServiceInject]
        public GameTask_LoadGameData(GameRepository repository, IGameMediator[] mediators)
        {
            this.repository = repository;
            this.mediators = mediators;
        }

        async void ILoadingTask.Do(Action<LoadingResult> callback)
        {
            await this.repository.LoadState();
            
            for (int i = 0, count = this.mediators.Length; i < count; i++)
            {
                var mediator = this.mediators[i];
                mediator.SetupData(this.repository);
            }
            
            callback?.Invoke(LoadingResult.Success());
        }
    }
}