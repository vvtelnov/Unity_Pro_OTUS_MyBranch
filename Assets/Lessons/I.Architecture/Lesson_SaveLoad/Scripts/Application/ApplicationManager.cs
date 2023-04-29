using System.Collections;
using GameSystem;
using Services;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Lessons.Architecture.SaveLoad
{
    //Тестовый класс... Нарушает принципы SOLID
    public sealed class ApplicationManager : MonoBehaviour
    {
        [SerializeField]
        private ServiceInstaller serviceInstaller;

        private bool applicationLoaded;

        private GameContext gameContext;

        [Button]
        public void LoadApplication()
        {
            if (!this.applicationLoaded)
            {
                this.StartCoroutine(this.LoadRoutine());
            }
        }

        private IEnumerator LoadRoutine()
        {
            this.InstallServices();
            yield return this.LoadGameScene();
            this.LoadGameData();
            this.StartGame();
            this.applicationLoaded = true;
        }

        private void InstallServices()
        {
            this.serviceInstaller.InstallServices();
            ServiceInjector.ResolveDependencies();
        }

        private IEnumerator LoadGameScene()
        {
            const string sceneId = "Lessons/SaveLoad/GameScene";
            var operation = Addressables.LoadSceneAsync(sceneId, LoadSceneMode.Additive);
            yield return operation;
            
            this.gameContext = FindObjectOfType<GameContext>();
        }

        private void LoadGameData()
        {
            var dataLoaders = ServiceLocator.GetServices<IGameDataLoader>();
            foreach (var dataLoader in dataLoaders)
            {
                dataLoader.LoadData(this.gameContext);
            }
        }

        private void StartGame()
        {
            this.gameContext.InitGame();
            this.gameContext.ReadyGame();
            this.gameContext.StartGame();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                this.SaveGameData();
            }
        }

        private void OnApplicationQuit()
        {
            this.SaveGameData();
        }

        private void SaveGameData()
        {
            if (!this.applicationLoaded)
            {
                return;
            }

            var dataSavers = ServiceLocator.GetServices<IGameDataSaver>();
            foreach (var dataSaver in dataSavers)
            {
                dataSaver.SaveData(this.gameContext);
            }
        }
    }
}