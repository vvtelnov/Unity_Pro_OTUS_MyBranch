using DG.Tweening;
using GameSystem;
using TMPro;
using UnityEngine;

namespace Lessons.Architecture.MVO
{
    public sealed class MoneyWidget : MonoBehaviour,
        IGameInitElement,
        IGameFinishElement
    {
        [SerializeField]
        private TextMeshProUGUI moneyText; //Логика представления

        private MoneyStorage moneyStorage; //Логика взаимодействия с системой

        [GameInject]
        public void Construct(MoneyStorage moneyStorage)
        {
            this.moneyStorage = moneyStorage;
        }

        void IGameInitElement.InitGame()
        {
            this.moneyText.text = this.moneyStorage.Money.ToString();
            this.moneyStorage.OnMoneyChanged += this.OnMoneyChanged; //Логика взаимодействия с системой
        }

        void IGameFinishElement.FinishGame()
        {
            this.moneyStorage.OnMoneyChanged -= this.OnMoneyChanged;
        }

        private void OnMoneyChanged(int money)
        {
            this.moneyText.text = money.ToString(); //Логика представления
            DOTween.Sequence()
                .Append(this.moneyText.transform.DOScale(new Vector3(1.1f, 1.1f, 1.0f), 0.1f))
                .Append(this.moneyText.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.3f));
        }
    }
}