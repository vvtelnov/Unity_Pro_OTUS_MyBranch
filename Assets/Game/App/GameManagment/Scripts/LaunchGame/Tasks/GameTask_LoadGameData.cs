using System;
using Services;

namespace Game.App
{
    public sealed class GameTask_LoadGameData : ILoadingTask
    {
        private readonly GameContainer gameContainer;

        private readonly IGameLoadDataListener[] loadListeners;

        [ServiceInject]
        public GameTask_LoadGameData(GameContainer gameContainer, IGameLoadDataListener[] loadListeners)
        {
            this.gameContainer = gameContainer;
            this.loadListeners = loadListeners;
        }

        public void Do(Action<LoadingResult> callback)
        {
            for (int i = 0, count = this.loadListeners.Length; i < count; i++)
            {
                var listener = this.loadListeners[i];
                listener.OnLoadData(this.gameContainer);
            }

            callback?.Invoke(LoadingResult.Success());
        }
    }
}