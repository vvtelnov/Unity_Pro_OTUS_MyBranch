using System;
using System.Threading.Tasks;
using Services;

namespace Game.App
{
    public sealed class GameTask_ReadyGame : ILoadingTask
    {
        private readonly GameContainer gameContainer;

        [ServiceInject]
        public GameTask_ReadyGame(GameContainer gameContainer)
        {
            this.gameContainer = gameContainer;
        }

        public void Do(Action<LoadingResult> callback)
        {
            this.gameContainer.ReadyGame();
            callback.Invoke(LoadingResult.Success());
        }
    }
}