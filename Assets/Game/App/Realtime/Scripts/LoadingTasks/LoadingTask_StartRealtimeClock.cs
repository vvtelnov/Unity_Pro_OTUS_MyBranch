using System;
using Services;

namespace Game.App
{
    public sealed class LoadingTask_StartRealtimeClock : ILoadingTask
    {
        public async void Do(Action<LoadingResult> callback)
        {
            var sessionStarter = ServiceLocator.GetService<RealtimeClockStarter>();
            await sessionStarter.StartClockAsync();
            callback?.Invoke(LoadingResult.Success());
        }
    }
}