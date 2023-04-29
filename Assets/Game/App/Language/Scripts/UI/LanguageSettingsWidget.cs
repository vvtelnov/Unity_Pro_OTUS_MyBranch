using System.Collections.Generic;
using Game.App;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.UI
{
    public sealed class LanguageSettingsWidget : MonoBehaviour
    {
        [SerializeField]
        private TMP_Dropdown dropdown;

        [FormerlySerializedAs("config")]
        [SerializeField]
        private LanguageCatalog catalog;

        private void Awake()
        {
            this.InitDropdown();
        }

        private void OnEnable()
        {
            this.dropdown.onValueChanged.AddListener(this.OnLanguageChanged);
        }

        private void OnDisable()
        {
            this.dropdown.onValueChanged.RemoveListener(this.OnLanguageChanged);
        }

        private void OnLanguageChanged(int languageIndex)
        {
            LanguageInfo info = this.catalog.GetLanguage(languageIndex);
            LanguageManager.CurrentLanguage = info.language;
        }
        
        private void InitDropdown()
        {
            var languagesConfigs = this.catalog.GetLanguages();
            var currentLanguage = LanguageManager.CurrentLanguage;
            var count = languagesConfigs.Length;

            var options = new List<string>(count);
            var targetIndex = 0;

            for (var i = 0; i < count; i++)
            {
                LanguageInfo info = languagesConfigs[i];
                options.Add(info.title.ToUpper());

                if (info.language == currentLanguage)
                {
                    targetIndex = i;
                }
            }

            this.dropdown.ClearOptions();
            this.dropdown.AddOptions(options);
            this.dropdown.value = targetIndex;
        }
    }
}