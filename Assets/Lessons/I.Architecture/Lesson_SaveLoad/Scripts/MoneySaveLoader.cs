using Game.Gameplay.Player;
using UnityEngine;

namespace Lessons.Architecture.SaveLoad
{
    public sealed class MoneySaveLoader : MonoBehaviour
    {
        [SerializeField]
        private MoneyStorage moneyStorage;

        private void Awake()
        {
            var money = PlayerPrefs.GetInt("Lesson/Money");
            this.moneyStorage.SetupMoney(money);
            Debug.Log($"<color=green>Money loaded: {money}!</color>");
        }

        private void OnApplicationQuit()
        {
            var money = this.moneyStorage.Money;
            PlayerPrefs.SetInt("Lesson/Money", money);
            Debug.Log($"<color=green>Money saved: {money}!</color>");
        }
    }
}