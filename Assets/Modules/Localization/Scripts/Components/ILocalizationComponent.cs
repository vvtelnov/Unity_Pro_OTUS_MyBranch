using UnityEngine;

namespace LocalizationModule
{
    public interface ILocalizationComponent
    {
        void UpdateLanguage(SystemLanguage language);
    }
}