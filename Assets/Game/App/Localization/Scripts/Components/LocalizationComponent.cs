using Game.App;
using LocalizationModule;
using Sirenix.OdinInspector;
using UnityEngine;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Game.Localization
{
    public class LocalizationComponent : SerializedMonoBehaviour
    {
        [SerializeField]
        private ILocalizationComponent[] children = new ILocalizationComponent[0];

        protected virtual void OnEnable()
        {
            this.UpdateLanguage(LanguageManager.CurrentLanguage);
            LanguageManager.OnLanguageChanged += this.UpdateLanguage;
        }

        protected virtual void OnDisable()
        {
            LanguageManager.OnLanguageChanged -= this.UpdateLanguage;
        }

        protected virtual void UpdateLanguage(SystemLanguage language)
        {
            for (int i = 0, count = this.children.Length; i < count; i++)
            {
                var component = this.children[i];
                component.UpdateLanguage(language);
            }
        }
    }
}