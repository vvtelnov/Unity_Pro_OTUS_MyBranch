using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Game.UI;
using GameSystem;
using Services;
using UnityEngine;

namespace Lessons.Architecture.Loading
{
    [CreateAssetMenu(
        fileName = "LoadingTask_LaunchGame",
        menuName = "Lessons/Tasks/New LoadingTask_LaunchGame"
    )]
    public sealed class LoadingTask_LaunchGame : LoadingTask
    {
        [Range(0, 1)]
        [SerializeField]
        private float progress = 0.9f;
        
        private GameDataLoader dataLoader;

        [ServiceInject]
        public void Construct(GameDataLoader dataLoader)
        {
            this.dataLoader = dataLoader;
        }
        
        public async override UniTask<Result> Do()
        {
            //Инициализация игровой системы:
            var ctx = GameObject.FindWithTag(nameof(GameContext)).GetComponent<GameContext>();
            ctx.ConstructGame();

            //Загрузка данных:
            await this.dataLoader.LoadData(ctx);
            LoadingScreen.ReportProgress(this.progress);

            //Запуск игры:
            ctx.InitGame();
            ctx.ReadyGame();
            ctx.StartGame();

            return await UniTask.FromResult(new Result
            {
                success = true
            });
        }
    }
}