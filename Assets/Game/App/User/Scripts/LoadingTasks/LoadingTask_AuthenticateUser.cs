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

        void ILoadingTask.Do(Action<LoadingResult> callback)
        {
            this.userAuth.Authenticate(_ => callback.Invoke(LoadingResult.Success()));
        }
    }
}