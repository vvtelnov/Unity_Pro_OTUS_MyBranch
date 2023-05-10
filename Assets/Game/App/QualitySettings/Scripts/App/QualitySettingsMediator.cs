using Services;

namespace Game.App
{
    public sealed class QualitySettingsMediator :
        IAppInitListener,
        IAppStartListener,
        IAppQuitListener
    {
        private const string PREFS_KEY = "QualitySettingsData";

        void IAppInitListener.Init()
        {
            if (PlayerPreferences.TryLoad(PREFS_KEY, out QualitySettingsData data))
            {
                QualitySettingsManager.SetLevel(data.qualityLevel);
            }
        }

        void IAppStartListener.Start()
        {
            QualitySettingsManager.OnLevelChanged += this.SaveSettings;
        }

        void IAppQuitListener.OnQuit()
        {
            QualitySettingsManager.OnLevelChanged -= this.SaveSettings;
        }

        private void SaveSettings(int level)
        {
            var data = new QualitySettingsData
            {
                qualityLevel = level
            };

            PlayerPreferences.Save(PREFS_KEY, data);
        }
    }
}