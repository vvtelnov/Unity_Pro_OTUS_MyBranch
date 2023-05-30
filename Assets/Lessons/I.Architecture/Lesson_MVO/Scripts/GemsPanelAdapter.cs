using GameSystem;
using UnityEngine;

namespace Lessons.Architecture.MVO
{
    public sealed class GemsPanelAdapter : MonoBehaviour, 
        IGameInitElement,
        IGameFinishElement
    {
        [SerializeField]
        private CurrencyPanel currencyPanel;

        private GemsStorage gemsStorage;

        [GameInject]
        public void Construct(GemsStorage gemsStorage)
        {
            this.gemsStorage = gemsStorage;
        }

        void IGameInitElement.InitGame()
        {
            this.currencyPanel.SetupCurrency(this.gemsStorage.Gems.ToString());
            this.gemsStorage.OnGemsChanged += this.OnGemsChanged;
        }

        void IGameFinishElement.FinishGame()
        {
            this.gemsStorage.OnGemsChanged -= this.OnGemsChanged;
        }

        private void OnGemsChanged(short gems)
        {
            this.currencyPanel.UpdateCurrency(gems.ToString());
        }
    }
}