using Cysharp.Threading.Tasks;
using Lessons.Architecture.Loading;
using Services;
using UnityEngine;

namespace Lessons.Architecture.SaveLoad
{
    [CreateAssetMenu(
        fileName = "LoadingTask_LoadData",
        menuName = "Lessons/Tasks/New LoadingTask_LoadData"
    )]
    public sealed class LoadingTask_LoadData : LoadingTask
    {
        private SaveLoadManager saveLoadManager;

        [ServiceInject]
        public void Construct(SaveLoadManager saveLoadManager)
        {
            this.saveLoadManager = saveLoadManager;
        }

        public override UniTask<Result> Do()
        {
            this.saveLoadManager.LoadGame();
            return UniTask.FromResult<Result>(new Result {success = true});
        }
    }
}