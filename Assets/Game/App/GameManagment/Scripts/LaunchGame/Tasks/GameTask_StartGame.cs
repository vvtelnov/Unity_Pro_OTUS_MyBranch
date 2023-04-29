using System;
using Services;

namespace Game.App
{
    public sealed class GameTask_StartGame : ILoadingTask
    {
        private readonly GameContainer gameContainer;

        [ServiceInject]
        public GameTask_StartGame(GameContainer gameContainer)
        {
            this.gameContainer = gameContainer;
        }
    
        public void Do(Action<LoadingResult> callback)
        {
            this.gameContainer.StartGame();
            callback?.Invoke(LoadingResult.Success());
        }
    }
}