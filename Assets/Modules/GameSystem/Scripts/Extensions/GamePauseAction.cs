using UnityEngine;

namespace GameSystem.Extensions
{
    [AddComponentMenu("GameSystem/Action «Pause Game»")]
    public sealed class GamePauseAction : MonoBehaviour, IGameAttachElement
    {
        private GameContext gameContext;

        [ContextMenu("Pause Game")]
        public void PauseGame()
        {
            if (this.gameContext.CurrentState == GameContext.State.PLAY)
            {
                this.gameContext.PauseGame();
            }
        }

        void IGameAttachElement.AttachGame(GameContext context)
        {
            this.gameContext = context;
        }
    }
}