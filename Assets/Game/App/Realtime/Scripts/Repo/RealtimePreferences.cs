namespace Game.App
{
    public sealed class RealtimePreferences
    {
        private const string PREFS_KEY = "UserSessionData";
     
        public bool LoadData(out RealtimeData data)
        {
            if (PlayerPreferences.KeyExists(PREFS_KEY))
            {
                data = PlayerPreferences.Load<RealtimeData>(PREFS_KEY);
                return true;
            }

            data = default;
            return false;
        }

        public void SaveData(RealtimeData data)
        {
            PlayerPreferences.Save(PREFS_KEY, data);
        }
    }
}