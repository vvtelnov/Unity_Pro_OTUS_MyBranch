using System;
using LocalizationModule;
using TMPro;
using UnityEngine;

namespace Game.Localization
{
    [Serializable]
    public sealed class TMProUGUIComponent_TranslationText : ILocalizationComponent
    {
        [SerializeField]
        private TextMeshProUGUI text;

        [TranslationKey]
        [SerializeField]
        private string key;

        public TMProUGUIComponent_TranslationText()
        {
        }

        public TMProUGUIComponent_TranslationText(TextMeshProUGUI text, string key)
        {
            this.text = text;
            this.key = key;
        }

        public void UpdateLanguage(SystemLanguage language)
        {
            var text = LocalizationManager.GetText(this.key, language);
            this.text.text = text;
        }   
    }
}