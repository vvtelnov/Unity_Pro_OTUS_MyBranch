using System;
using Services;

namespace Game.App
{
    public sealed class GameTask_InitGame : ILoadingTask
    {
        private readonly GameContainer gameContainer;

        [ServiceInject]
        public GameTask_InitGame(GameContainer gameContainer)
        {
            this.gameContainer = gameContainer;
        }

        public void Do(Action<LoadingResult> callback)
        {
            this.gameContainer.InitGame();
            callback?.Invoke(LoadingResult.Success());
        }
    }
}