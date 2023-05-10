using System;
using GameSystem;
using Services;
using UnityEngine;

namespace Game.App
{
    public sealed class GameTask_SetGameContext : ILoadingTask
    {
        private readonly GameFacade gameFacade;

        [ServiceInject]
        public GameTask_SetGameContext(GameFacade gameFacade)
        {
            this.gameFacade = gameFacade;
        }
    
        public void Do(Action<LoadingResult> callback)
        {
            var gameContext = GameObject.FindObjectOfType<GameContext>();
            this.gameFacade.SetGame(gameContext);
            callback?.Invoke(LoadingResult.Success());
        }
    }
}