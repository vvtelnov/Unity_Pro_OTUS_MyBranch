using UnityEngine;

namespace GameSystem.Extensions
{
    [AddComponentMenu("GameSystem/Action «Resume Game»")]
    public sealed class GameResumeAction : MonoBehaviour, IGameAttachElement
    {
        private GameContext gameContext;

        [ContextMenu("Resume Game")]
        public void ResumeGame()
        {
            if (this.gameContext.CurrentState == GameContext.State.PAUSE)
            {
                this.gameContext.ResumeGame();
            }
        }

        void IGameAttachElement.AttachGame(GameContext context)
        {
            this.gameContext = context;
        }
    }
}