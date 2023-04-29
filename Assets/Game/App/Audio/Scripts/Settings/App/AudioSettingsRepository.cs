namespace Game.App
{
    public sealed class AudioSettingsRepository : DataRepository<AudioSettingsData>
    {
        protected override string Key => "AudioSettingsData";

        public bool LoadSettings(out AudioSettingsData data)
        {
            return this.LoadData(out data);
        }

        public void SaveSettings(AudioSettingsData data)
        {
            this.SaveData(data);
        }
    }
}