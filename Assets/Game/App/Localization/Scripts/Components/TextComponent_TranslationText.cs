using System;
using LocalizationModule;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Localization
{
    [Serializable]
    public sealed class TextComponent_TranslationText : ILocalizationComponent
    {
        [SerializeField]
        private Text text;
        
        [TranslationKey]
        [SerializeField]
        private string key;
        
        public void UpdateLanguage(SystemLanguage language)
        {
            this.text.text = LocalizationManager.GetText(this.key, language);
        }
    }
}