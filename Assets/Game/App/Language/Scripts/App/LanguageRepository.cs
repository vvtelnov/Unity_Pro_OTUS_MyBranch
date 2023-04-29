using UnityEngine;

namespace Game.App
{
    public sealed class LanguageRepository : DataRepository<LanguageData>
    {
        protected override string Key => "LanguageData";

        public bool LoadLanguage(out SystemLanguage language)
        {
            if (this.LoadData(out var data))
            {
                language = data.language;
                return true;
            }

            language = default;
            return false;
        }

        public void SaveLanguage(SystemLanguage language)
        {
            var data = new LanguageData
            {
                language = language
            };
            this.SaveData(data);
        }
    }
}