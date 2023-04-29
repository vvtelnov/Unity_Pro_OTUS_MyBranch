using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Lessons.Architecture.MVA
{
    //VIEW
    public sealed class CurrencyPanel : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI moneyText;

        public void SetupMoney(string money)
        {
            this.moneyText.text = money;
        }

        public void UpdateMoney(string money)
        {
            this.moneyText.text = money;
            this.AnimateTextBounce();
        }
        
        private void AnimateTextBounce()
        {
            //Scale animation:
            DOTween
                .Sequence()
                .Append(this.moneyText.transform.DOScale(new Vector3(1.1f, 1.1f, 1.0f), 0.1f))
                .Append(this.moneyText.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.3f));
        }
    }
}