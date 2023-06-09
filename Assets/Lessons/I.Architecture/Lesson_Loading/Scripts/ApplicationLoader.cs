using Asyncoroutine;
using Game.UI;
using GameSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lessons.Architecture.Loading
{
    public sealed class ApplicationLoader : MonoBehaviour
    {
        [Button]
        public async void LoadApp()
        {
            await SceneManager.LoadSceneAsync("Game/Scenes/GameScene");
            
            var ctx = GameObject.FindWithTag("GameContext").GetComponent<GameContext>();
            ctx.ConstructGame();
            ctx.InitGame();
            ctx.ReadyGame();
            ctx.StartGame();

            LoadingScreen.Hide();
        }
    }
}