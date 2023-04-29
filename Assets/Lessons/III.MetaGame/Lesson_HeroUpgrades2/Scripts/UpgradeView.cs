using Game.Meta;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.MetaGame.Upgrades2
{
    public sealed class UpgradeView : MonoBehaviour
    {
        public UpgradeButton Button
        {
            get { return this.button; }
        }

        [SerializeField]
        private TextMeshProUGUI title;

        [SerializeField]
        private TextMeshProUGUI level;

        [SerializeField]
        private TextMeshProUGUI stats;

        [SerializeField]
        private Image icon;

        [SerializeField]
        private UpgradeButton button;

        public void SetTitle(string title)
        {
            this.title.text = title;
        }

        public void SetLevel(string level)
        {
            this.level.text = level;
        }

        public void SetStats(string stats)
        {
            this.stats.text = stats;
        }

        public void SetIcon(Sprite icon)
        {
            this.icon.sprite = icon;
        }
    }
}