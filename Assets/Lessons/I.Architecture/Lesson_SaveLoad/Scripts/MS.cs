// namespace Lessons.Architecture.SaveLoad
// {
//     public class MS
//     {
//         public sealed class MoneySaveLoader : ISaveLoader
//         {
//             void ISaveLoader.LoadGame(GameContext context)
//             {
//                 var money = PlayerPrefs.GetInt("Lesson/Money");
//                 var moneyStorage = context.GetService<MoneyStorage>();
//                 moneyStorage.SetupMoney(money);
//                 Debug.Log($"<color=green>Money loaded: {money}!</color>");
//             }
//
//             void ISaveLoader.SaveGame(GameContext context)
//             {
//                 var moneyStorage = context.GetService<MoneyStorage>();
//                 var money = moneyStorage.Money;
//                 PlayerPrefs.SetInt("Lesson/Money", money);
//                 Debug.Log($"<color=green>Money saved: {money}!</color>");
//             }
//         }
//     }
//     }
// }