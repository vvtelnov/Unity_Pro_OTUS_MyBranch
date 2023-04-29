using GameSystem;
using UnityEngine;

namespace Lessons.Architecture.MVA
{
    public sealed class GemsPanelAdapter : MonoBehaviour,
        IGameConstructElement,
        IGameReadyElement,
        IGameFinishElement
    {
        [SerializeField]
        private CurrencyPanel gemsPanel;

        private GemsStorage gemsStorage;

        void IGameConstructElement.ConstructGame(GameContext context)
        {
            this.gemsStorage = context.GetService<GemsStorage>();
            this.gemsPanel.SetupMoney(this.gemsStorage.Gems.ToString());
        }

        void IGameReadyElement.ReadyGame()
        {
            this.gemsStorage.OnGemsChanged += this.OnGemsChanged;
        }

        void IGameFinishElement.FinishGame()
        {
            this.gemsStorage.OnGemsChanged -= this.OnGemsChanged;
        }

        private void OnGemsChanged(int gems)
        {
            this.gemsPanel.UpdateMoney(gems.ToString());
        }
    }
}