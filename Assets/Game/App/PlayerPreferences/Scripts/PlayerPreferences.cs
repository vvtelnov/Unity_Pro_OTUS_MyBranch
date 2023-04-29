namespace Game.App
{
    public static class PlayerPreferences
    {
        public static bool KeyExists(string key)
        {
            return ES3.KeyExists(key);
        }

        public static void Save<T>(string key, T data)
        {
            ES3.Save(key, data);
        }

        public static T Load<T>(string key)
        {
            return ES3.Load<T>(key);
        }

        public static void Remove(string key)
        {
            ES3.DeleteKey(key);
        }
    }
}