using Asyncoroutine;
using Game.GameEngine;
using Game.UI;
using GameSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lessons.Architecture.Loading
{
    public sealed class ApplicationLoader : MonoBehaviour
    {
        public async void Start()
        {
            await SceneManager.LoadSceneAsync("Game/Scenes/GameScene");

            var popupCatalog = Resources.Load<PopupCatalog>(nameof(PopupCatalog));
            await popupCatalog.LoadAssets();

            var ctx = GameObject.FindWithTag(nameof(GameContext)).GetComponent<GameContext>();
            ctx.ConstructGame();
            ctx.InitGame();
            ctx.ReadyGame();
            ctx.StartGame();

            LoadingScreen.Hide();
        }
    }
}