using System;
using Services;

namespace Game.App
{
    public sealed class LoadingTask_AuthenticateUser : ILoadingTask
    {
        private readonly UserAuthenticator userAuth;

        [ServiceInject]
        public LoadingTask_AuthenticateUser(UserAuthenticator userAuth)
        {
            this.userAuth = userAuth;
        }

        async void ILoadingTask.Do(Action<LoadingResult> callback)
        {
            await this.userAuth.Authenticate();
            callback.Invoke(LoadingResult.Success());
        }
    }
}