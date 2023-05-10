using JetBrains.Annotations;
using Services;
using UnityEngine;

namespace Game.App
{
    [UsedImplicitly]
    public sealed class LanguageMediator :
        IAppInitListener,
        IAppStartListener,
        IAppQuitListener
    {
        private const string CONFIG_PATH = "LanguageCatalog";

        void IAppInitListener.Init()
        {
            var language = this.LoadLanguage();
            LanguageManager.CurrentLanguage = language;
        }

        void IAppStartListener.Start()
        {
            LanguageManager.OnLanguageChanged += this.SaveLanguage;
        }

        void IAppQuitListener.OnQuit()
        {
            LanguageManager.OnLanguageChanged -= this.SaveLanguage;
        }

        private SystemLanguage LoadLanguage()
        {
            if (ES3.KeyExists(nameof(LanguageData)))
            {
                return ES3.Load<SystemLanguage>(nameof(LanguageData));
            }
            
            var language = Application.systemLanguage;
            
            var catalog = Resources.Load<LanguageCatalog>(CONFIG_PATH);
            if (!catalog.LanguageExists(language))
            {
                language = catalog.defaultLanguage;
            }

            return language;
        }

        private void SaveLanguage(SystemLanguage language)
        {
            ES3.Save(nameof(LanguageData), language);
        }
    }
}