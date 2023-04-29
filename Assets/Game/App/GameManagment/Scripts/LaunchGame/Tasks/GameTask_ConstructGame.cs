using System;
using Services;

namespace Game.App
{
    public class GameTask_ConstructGame : ILoadingTask
    {
        private readonly GameContainer gameContainer;

        [ServiceInject]
        public GameTask_ConstructGame(GameContainer gameContainer)
        {
            this.gameContainer = gameContainer;
        }
    
        public void Do(Action<LoadingResult> callback)
        {
            this.gameContainer.ConstructGame();
            callback?.Invoke(LoadingResult.Success());
        }
    }
}