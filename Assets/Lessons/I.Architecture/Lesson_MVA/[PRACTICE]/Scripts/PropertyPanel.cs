using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.MVA
{
    public sealed class PropertyPanel : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI valueText;

        [SerializeField]
        private Image iconImage;

        public void SetIcon(Sprite icon)
        {
            this.iconImage.sprite = icon;
        }

        public void SetupValue(string text)
        {
            this.valueText.text = text;
        }

        public void UpdateValue(string text)
        {
            this.valueText.text = text;
            
            //Анимации:
            DOTween
                .Sequence()
                .Append(this.valueText.DOColor(Color.red, 0.1f))
                .Append(this.valueText.DOColor(Color.black, 0.1f))
                .Append(this.valueText.DOColor(Color.red, 0.1f))
                .Append(this.valueText.DOColor(Color.black, 0.1f));
        }
    }
}