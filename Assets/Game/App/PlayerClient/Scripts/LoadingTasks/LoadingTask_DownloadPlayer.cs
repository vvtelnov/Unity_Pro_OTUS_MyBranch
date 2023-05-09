using System;
using Services;

namespace Game.App.LoadingTasks
{
    public sealed class LoadingTask_DownloadPlayer : ILoadingTask
    {
        private readonly UserAuthenticator userAuth;

        private readonly PlayerClient playerLoader;

        [ServiceInject]
        public LoadingTask_DownloadPlayer(UserAuthenticator userAuth, PlayerClient playerLoader)
        {
            this.userAuth = userAuth;
            this.playerLoader = playerLoader;
        }

        void ILoadingTask.Do(Action<LoadingResult> callback)
        {
            if (!this.userAuth.IsAuthorized)
            {
                callback.Invoke(LoadingResult.Success()); //Not critical if user not authrorized :)
                return;
            }

            this.playerLoader.DownloadState(
                onSuccess: () => callback.Invoke(LoadingResult.Success()),
                onError: () => callback.Invoke(LoadingResult.Success())
            );
        }
    }
}