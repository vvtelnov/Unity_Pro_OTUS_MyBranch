using System;
using System.Threading.Tasks;
using GameSystem;
using Services;
using UnityEngine;

namespace Game.App
{
    public sealed class GameTask_SetupGame : ILoadingTask
    {
        private readonly GameFacade gameFacade;

        [ServiceInject]
        public GameTask_SetupGame(GameFacade gameFacade)
        {
            this.gameFacade = gameFacade;
        }
    
        public void Do(Action<LoadingResult> callback)
        {
            var gameContext = GameObject.FindObjectOfType<GameContext>();
            this.gameFacade.SetupGame(gameContext);
            callback?.Invoke(LoadingResult.Success());
        }
    }
}