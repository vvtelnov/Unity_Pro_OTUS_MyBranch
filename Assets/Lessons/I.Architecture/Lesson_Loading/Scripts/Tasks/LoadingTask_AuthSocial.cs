using System.Threading.Tasks;
using Game.UI;
using UnityEngine;

namespace Lessons.Architecture.Loading
{
    [CreateAssetMenu(
        fileName = "LoadingTask_AuthSocial",
        menuName = "Lessons/Tasks/New LoadingTask_AuthSocial"
    )]
    public sealed class LoadingTask_AuthSocial : LoadingTask
    {
        [SerializeField]
        private string error = "Ошибка авторизации!";

        [Range(0, 1)]
        [SerializeField]
        private float progress = 0.2f;
        
        public override Task<Result> Do()
        {
            var tcs = new TaskCompletionSource<Result>();
            
            Social.localUser.Authenticate(success =>
            {
                tcs.SetResult(new Result
                {
                    success = success,
                    error = this.error,
                });
                
                LoadingScreen.ReportProgress(this.progress);
            });

            return tcs.Task;
        }
    }
}