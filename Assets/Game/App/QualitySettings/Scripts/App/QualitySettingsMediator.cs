using Services;

namespace Game.App
{
    public sealed class QualitySettingsMediator :
        IAppInitListener,
        IAppStartListener,
        IAppQuitListener
    {
        [ServiceInject]
        private QualitySettingsRepository repository;

        
        void IAppInitListener.Init()
        {
            if (this.repository.LoadSettings(out var data))
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
            this.repository.SaveSettings(data);
        }
    }
}