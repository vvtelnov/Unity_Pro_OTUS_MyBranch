using System.Threading.Tasks;
using UnityEngine;

namespace Lessons.Architecture.Loading
{
    [CreateAssetMenu(
        fileName = "LoadingTask_StartPlugins",
        menuName = "Lessons/Tasks/New LoadingTask_StartPlugins"
    )]
    public sealed class LoadingTask_StartPlugins : LoadingTask
    {
        public override Task<Result> Do()
        {
            AppsFlyer.startSDK();

            var tcs = new TaskCompletionSource<Result>();
            
            FB.Init(
                onSuccess: () => tcs.SetResult(new Result
                {
                    success = true,
                }),
                onError: err => tcs.SetResult(new Result
                {
                    success = false,
                    error = err,
                })
            );

            return tcs.Task;
        }
    }
}