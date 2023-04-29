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

        [ServiceInject]
        private LanguageRepository repository;

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
            if (this.repository.LoadLanguage(out var language))
            {
                return language;
            }
            
            language = Application.systemLanguage;
            
            var catalog = Resources.Load<LanguageCatalog>(CONFIG_PATH);
            if (!catalog.LanguageExists(language))
            {
                language = catalog.defaultLanguage;
            }

            return language;
        }

        private void SaveLanguage(SystemLanguage language)
        {
            this.repository.SaveLanguage(language);
        }
    }
}