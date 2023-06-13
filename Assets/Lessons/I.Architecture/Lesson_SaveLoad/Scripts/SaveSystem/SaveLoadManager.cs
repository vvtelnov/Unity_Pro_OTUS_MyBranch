using GameSystem;
using Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Architecture.SaveLoad
{
    public sealed class SaveLoadManager : MonoBehaviour
    {
        private ISaveLoader[] saveLoaders;
        private GameRepository repository;

        [ServiceInject]
        public void Construct(ISaveLoader[] saveLoaders, GameRepository repository)
        {
            this.saveLoaders = saveLoaders;
            this.repository = repository;
        }

        [Button]
        public void LoadGame()
        {
            this.repository.LoadState();
            
            var gameContext = FindObjectOfType<GameContext>();
            foreach (var saveLoader in this.saveLoaders)
            {
                saveLoader.LoadGame(this.repository, gameContext);
            }
        }

        [Button]
        public void SaveGame()
        {
            var gameContext = FindObjectOfType<GameContext>();
            foreach (var saveLoader in this.saveLoaders)
            {
                saveLoader.SaveGame(this.repository, gameContext);
            }
            
            this.repository.SaveState();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                this.SaveGame();
            }
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                this.SaveGame();
            }
        }

        private void OnApplicationQuit()
        {
            this.SaveGame();
        }
    }
}