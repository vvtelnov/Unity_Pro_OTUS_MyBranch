using System;
using Services;

namespace Game.App
{
    public sealed class GameTask_LoadGameData : ILoadingTask
    {
        private readonly GameFacade gameFacade;

        private readonly IGameLoadDataListener[] loadListeners;

        [ServiceInject]
        public GameTask_LoadGameData(GameFacade gameFacade, IGameLoadDataListener[] loadListeners)
        {
            this.gameFacade = gameFacade;
            this.loadListeners = loadListeners;
        }

        public void Do(Action<LoadingResult> callback)
        {
            for (int i = 0, count = this.loadListeners.Length; i < count; i++)
            {
                var listener = this.loadListeners[i];
                listener.OnLoadData(this.gameFacade);
            }

            callback?.Invoke(LoadingResult.Success());
        }
    }
}