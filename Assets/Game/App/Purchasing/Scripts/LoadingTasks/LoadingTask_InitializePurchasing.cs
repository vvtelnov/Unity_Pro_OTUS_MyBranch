using System;
using System.Threading.Tasks;
using Services;

namespace Game.App.Loading
{
    public sealed class LoadingTask_InitializePurchasing : ILoadingTask
    {
        public void Do(Action<LoadingResult> callback)
        {
            PurchasingInitializer.Init(result =>
            {
                if (result.isSuccess)
                {
                    callback?.Invoke(LoadingResult.Success());
                }
                else
                {
                    callback?.Invoke(LoadingResult.Fail(result.error.ToString()));
                }
            });
        }
    }
}