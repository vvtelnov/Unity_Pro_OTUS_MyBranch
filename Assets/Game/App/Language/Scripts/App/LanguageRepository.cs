using UnityEngine;

namespace Game.App
{
    public sealed class LanguageRepository : Repository<LanguageData>
    {
        protected override string PrefsKey => "LanguageData";

        public bool LoadLanguage(out SystemLanguage language)
        {
            if (this.LoadData(out LanguageData data))
            {
                language = data.language;
                return true;
            }

            language = default;
            return false;
        }

        public void SaveLanguage(SystemLanguage language)
        {
            this.SaveData(new LanguageData
            {
                language = language
            });
        }
    }
}