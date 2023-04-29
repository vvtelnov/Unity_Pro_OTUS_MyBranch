using Services;

namespace Game.App
{
    public sealed class AudioSettingsMediator : 
        IAppInitListener,
        IAppPauseListener,
        IAppQuitListener
    {
        [ServiceInject]
        private AudioSettingsRepository repository;

        void IAppInitListener.Init()
        {
            this.LoadSettings();
        }
        
        void IAppPauseListener.OnPaused()
        {
            this.SaveSettings();
        }

        void IAppQuitListener.OnQuit()
        {
            this.SaveSettings();
        }

        private void LoadSettings()
        {
            if (this.repository.LoadSettings(out AudioSettingsData data))
            {
                AudioSettingsManager.SetMusicVolume(data.musicVolume);
                AudioSettingsManager.SetSoundVolume(data.soundVolume);
            }
            else
            {
                AudioSettingsManager.SetMusicVolumeDefault();
                AudioSettingsManager.SetSoundVolumeDefault();
            }
        }

        private void SaveSettings()
        {
            AudioSettingsData data = new AudioSettingsData
            {
                musicVolume = AudioSettingsManager.MusicVolume,
                soundVolume = AudioSettingsManager.SoundVolume
            };
            this.repository.SaveSettings(data);
        }
    }
}