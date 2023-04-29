namespace Game.App
{
    public sealed class QualitySettingsRepository : DataRepository<QualitySettingsData>
    {
        protected override string Key => "QualitySettingsData";

        public bool LoadSettings(out QualitySettingsData data)
        {
            return this.LoadData(out data);
        }

        public void SaveSettings(QualitySettingsData data)
        {
            this.SaveData(data);
        }
    }
}