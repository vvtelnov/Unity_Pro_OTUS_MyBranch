using Newtonsoft.Json;
using UnityEngine;

namespace Game.App
{
    public static class PlayerPreferences
    {
        public static bool TryLoad<T>(string key, out T data)
        {
            if (PlayerPrefs.HasKey(key))
            {
                var json = PlayerPrefs.GetString(key);
                data = JsonConvert.DeserializeObject<T>(json);
                return true;
            }
            
            data = default;
            return false;
        }
        
        public static bool KeyExists(string key)
        {
            return PlayerPrefs.HasKey(key);
        }
        
        public static void Save(string key, object data)
        {
            var json = JsonConvert.SerializeObject(data);
            PlayerPrefs.SetString(key, json);
        }
        
        public static T Load<T>(string key)
        {
            var json = PlayerPrefs.GetString(key);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static void Remove(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        // public static bool TryLoad<T>(string key, out T data)
        // {
        //     if (ES3.KeyExists(key))
        //     {
        //         data = ES3.Load<T>(key);
        //         return true;
        //     }
        //     
        //     data = default;
        //     return false;
        // }
        //
        // public static bool KeyExists(string key)
        // {
        //     return ES3.KeyExists(key);
        // }
        //
        // public static void Save<T>(string key, T data)
        // {
        //     ES3.Save(key, data);
        // }
        //
        // public static void Save(string key, object data)
        // {
        //     ES3.Save(key, data);
        // }
        //
        // public static object Load(string key)
        // {
        //     return ES3.Load(key);
        // }
        //
        // public static T Load<T>(string key)
        // {
        //     return ES3.Load<T>(key);
        // }
        //
        // public static void Remove(string key)
        // {
        //     ES3.DeleteKey(key);
        // }
        
        // public static bool TryLoad(string key, out object data)
        // {
        //     if (ES3.KeyExists(key))
        //     {
        //         data = ES3.Load(key);
        //         return true;
        //     }
        //     
        //     data = default;
        //     return false;
        // }
    }
}