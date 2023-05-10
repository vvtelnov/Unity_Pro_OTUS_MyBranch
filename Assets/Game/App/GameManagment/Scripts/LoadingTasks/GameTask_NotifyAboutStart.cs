using System;
using Services;

namespace Game.App
{
    public sealed class GameTask_NotifyAboutStart : ILoadingTask
    {
        private readonly GameFacade gameFacade;

        private readonly IGameStartListener[] startListeners;
        
        [ServiceInject]
        public GameTask_NotifyAboutStart(GameFacade gameFacade, IGameStartListener[] startListeners)
        {
            this.gameFacade = gameFacade;
            this.startListeners = startListeners;
        }
    
        public void Do(Action<LoadingResult> callback)
        {
            for (int i = 0, count = this.startListeners.Length; i < count; i++)
            {
                var listener = this.startListeners[i];
                listener.OnStartGame(this.gameFacade);
            }

            callback?.Invoke(LoadingResult.Success());
        }
    }
}