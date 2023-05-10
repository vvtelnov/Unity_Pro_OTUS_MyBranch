using System;
using Services;

namespace Game.App
{
    public sealed class GameTask_StartGame : ILoadingTask
    {
        private readonly GameFacade gameFacade;

        [ServiceInject]
        public GameTask_StartGame(GameFacade gameFacade)
        {
            this.gameFacade = gameFacade;
        }
    
        public void Do(Action<LoadingResult> callback)
        {
            this.gameFacade.StartGame();
            callback?.Invoke(LoadingResult.Success());
        }
    }
}