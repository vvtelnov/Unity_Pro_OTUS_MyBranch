using System;
using System.Threading.Tasks;
using GameSystem;
using Services;
using UnityEngine;

namespace Game.App
{
    public sealed class GameTask_SetupGame : ILoadingTask
    {
        private readonly GameContainer gameContainer;

        [ServiceInject]
        public GameTask_SetupGame(GameContainer gameContainer)
        {
            this.gameContainer = gameContainer;
        }
    
        public void Do(Action<LoadingResult> callback)
        {
            var gameContext = GameObject.FindObjectOfType<GameContext>();
            this.gameContainer.SetupGame(gameContext);
            callback?.Invoke(LoadingResult.Success());
        }
    }
}