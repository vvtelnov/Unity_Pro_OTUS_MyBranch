using Asyncoroutine;
using Game.GameEngine;
using Game.UI;
using GameSystem;
using Purchasing;
using Services;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lessons.Architecture.Loading
{
    public sealed class ApplicationLoader : MonoBehaviour
    {
        public async void Start()
        {
            //Запуск плагинов:
            AppsFlyer.startSDK();
            FB.Init();

            //Инициализация встроенных покупок:
            await UnityServices.InitializeAsync();
            var purchaser = ServiceLocator.GetService<PurchaseManager>();
            purchaser.Initialize(result =>
            {
                if (result.isSuccess)
                {
                    Debug.Log("PURCHSING INITIALIZED");
                }
                else
                {
                    Debug.Log("PURCH FAILED");
                }
            });

            //Подключение к Social Platform:
            Social.localUser.Authenticate(b =>
            {
                if (b)
                {
                    Debug.Log("Авторизация успешна");
                }
                else
                {
                    Debug.Log("Ошибка авторизации");
                }
            });
            
            //Загрузка сцены:
            await SceneManager.LoadSceneAsync("Game/Scenes/GameScene");

            //Загрузка ассетов:
            var popupCatalog = Resources.Load<PopupCatalog>(nameof(PopupCatalog));
            await popupCatalog.LoadAssets();

            var ctx = GameObject.FindWithTag(nameof(GameContext)).GetComponent<GameContext>();
            ctx.ConstructGame();
            
            //Загрузка данных:
            await GameDataLoader.LoadData(ctx);

            ctx.InitGame();
            ctx.ReadyGame();
            ctx.StartGame();

            LoadingScreen.Hide();
        }
    }
}