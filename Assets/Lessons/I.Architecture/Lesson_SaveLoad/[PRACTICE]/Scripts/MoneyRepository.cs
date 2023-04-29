using UnityEngine;

namespace Lessons.Architecture.SaveLoad
{
    public sealed class MoneyRepository
    {
        private const string KEY = "Lesson/Money";
    
        public bool LoadMoney(out int money)
        {
            if (PlayerPrefs.HasKey(KEY))
            {
                money = PlayerPrefs.GetInt(KEY);
                return true;
            }

            money = default;
            return false;
        }

        public void SaveMoney(int money)
        {
            PlayerPrefs.SetInt(KEY, money);   
        }
    }
}