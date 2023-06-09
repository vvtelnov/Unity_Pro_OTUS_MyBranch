using System.Collections;
using Asyncoroutine;
using Game.GameEngine;
using Game.UI;
using GameSystem;
using Purchasing;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lessons.Architecture.Loading
{
    //1. Загрузка AppsFlyer & Facebook
    //2. Инициализация встроенных покупок
    //3. Авторизация в Gaming Services
    //4. Загрузка сцены игры
    //5. Загрузка попапов из Addressables
    //6. Загрузка данных игрока
    //7. Запуск игры
    public sealed class ApplicationLoader : MonoBehaviour
    {
        public void Start()
        {
            //Запуск плагинов:
            AppsFlyer.startSDK();
            FB.Init(this.OnFacebookInitialized, LoadingScreen.ReportError);
        }

        private async void OnFacebookInitialized()
        {
            //Инициализация встроенных покупок:
            await UnityServices.InitializeAsync();
            var purchasing = new PurchaseManager();
            purchasing.Initialize(result =>
            {
                if (result.isSuccess)
                {
                    this.OnPurchasingInitialized();
                }
                else
                {
                    LoadingScreen.ReportError("Встроенные покупки не загружены!");
                }
            });
        }

        private void OnPurchasingInitialized()
        {
            //Подключение к Social Platform:
            Social.localUser.Authenticate(b =>
            {
                if (b)
                {
                    this.OnAuthorizationSucceed();
                }
                else
                {
                    LoadingScreen.ReportError("Ошибка авторизации!");
                }
            });
        }

        private async void OnAuthorizationSucceed()
        {
            //Загрузка сцены:
            await this.LoadGameScene();

            //Загрузка попапов из Addressables:
            var popupCatalog = Resources.Load<PopupCatalog>(nameof(PopupCatalog));
            await popupCatalog.LoadAssets();
            LoadingScreen.ReportProgress(0.8f);

            //Инициализация игровой системы:
            var ctx = GameObject.FindWithTag(nameof(GameContext)).GetComponent<GameContext>();
            ctx.ConstructGame();

            //Загрузка данных:
            var dataLoader = new GameDataLoader();
            await dataLoader.LoadData(ctx);
            LoadingScreen.ReportProgress(0.9f);

            //Запуск игры:
            ctx.InitGame();
            ctx.ReadyGame();
            ctx.StartGame();

            LoadingScreen.ReportProgress(1f);
            await new WaitForSeconds(0.5f);
            LoadingScreen.Hide();
        }

        private IEnumerator LoadGameScene()
        {
            var operation = SceneManager.LoadSceneAsync("Game/Scenes/GameScene");
            while (!operation.isDone)
            {
                LoadingScreen.ReportProgress(operation.progress / 2);
                yield return null;
            }

            LoadingScreen.ReportProgress(0.5f);
        }
    }
}