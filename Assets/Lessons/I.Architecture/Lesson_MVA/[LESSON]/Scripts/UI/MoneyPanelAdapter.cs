using GameSystem;
using UnityEngine;

namespace Lessons.Architecture.MVA
{
    //ADAPTER
    public sealed class MoneyPanelAdapter : MonoBehaviour, 
        IGameConstructElement,
        IGameReadyElement,
        IGameFinishElement
    {
        [SerializeField]
        private CurrencyPanel moneyPanel;

        private MoneyStorage moneyStorage;

        void IGameConstructElement.ConstructGame(GameContext context)
        {
            this.moneyStorage = context.GetService<MoneyStorage>();
            this.moneyPanel.SetupMoney(this.moneyStorage.Money.ToString());
        }

        void IGameReadyElement.ReadyGame()
        {
            this.moneyStorage.OnMoneyChanged += this.OnMoneyChanged;
        }

        void IGameFinishElement.FinishGame()
        {
            this.moneyStorage.OnMoneyChanged -= this.OnMoneyChanged;
        }

        private void OnMoneyChanged(BigNumber money)
        {
            this.moneyPanel.UpdateMoney(money.ToString());
        }
    }
}