using System;
using Services;

namespace Game.App
{
    public sealed class GameTask_NotifyAboutStart : ILoadingTask
    {
        private readonly GameContainer gameContainer;

        private readonly IGameStartListener[] startListeners;
        
        [ServiceInject]
        public GameTask_NotifyAboutStart(GameContainer gameContainer, IGameStartListener[] startListeners)
        {
            this.gameContainer = gameContainer;
            this.startListeners = startListeners;
        }
    
        public void Do(Action<LoadingResult> callback)
        {
            for (int i = 0, count = this.startListeners.Length; i < count; i++)
            {
                var listener = this.startListeners[i];
                listener.OnStartGame(this.gameContainer);
            }

            callback?.Invoke(LoadingResult.Success());
        }
    }
}