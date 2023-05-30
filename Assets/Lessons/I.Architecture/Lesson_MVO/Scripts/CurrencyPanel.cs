using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Lessons.Architecture.MVO
{
    public sealed class CurrencyPanel : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI valueText; //Логика представления

        public void SetupCurrency(string currency)
        {
            this.valueText.text = currency;
        }

        public void UpdateCurrency(string currency)
        {
            this.valueText.text = currency; //Логика представления
            DOTween.Sequence()
                .Append(this.valueText.transform.DOScale(new Vector3(1.1f, 1.1f, 1.0f), 0.1f))
                .Append(this.valueText.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.3f));
        }
    }
}