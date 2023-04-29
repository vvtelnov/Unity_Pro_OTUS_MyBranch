// using DG.Tweening;
// using GameElements;
// using TMPro;
// using UnityEngine;
//
// namespace Lessons.Architecture.MVA
// {
//     public sealed class MoneyWidget : MonoBehaviour,
//         IGameInitElement,
//         IGameReadyElement,
//         IGameFinishElement
//     {
//         [SerializeField]
//         private TextMeshProUGUI moneyText;
//
//         private MoneyStorage moneyStorage;
//
//         void IGameInitElement.InitGame(IGameContext context)
//         {
//             this.moneyStorage = context.GetService<MoneyStorage>();
//             this.moneyText.text = this.moneyStorage.Money.ToString();
//         }
//
//         void IGameReadyElement.ReadyGame(IGameContext context)
//         {
//             this.moneyStorage.OnMoneyChanged += this.OnMoneyChanged;
//         }
//
//         void IGameFinishElement.FinishGame(IGameContext context)
//         {
//             this.moneyStorage.OnMoneyChanged -= this.OnMoneyChanged;
//         }
//
//         private void OnMoneyChanged(int money)
//         {
//             this.moneyText.text = money.ToString();
//             this.AnimateTextBounce();
//         }
//
//         private void AnimateTextBounce()
//         {
//             //Scale animation:
//             DOTween
//                 .Sequence()
//                 .Append(this.moneyText.transform.DOScale(new Vector3(1.1f, 1.1f, 1.0f), 0.1f))
//                 .Append(this.moneyText.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.3f));
//         }
//     }
// }