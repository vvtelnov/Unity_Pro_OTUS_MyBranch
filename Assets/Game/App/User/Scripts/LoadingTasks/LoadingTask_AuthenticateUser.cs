using System;
using Services;
using UnityEngine;

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
            this.userAuth.Authenticate(
                onSuccess: () =>
                {
                    Debug.Log("Auth success");
                    callback.Invoke(LoadingResult.Success());
                },
                onError: () =>
                {
                    Debug.Log("Auth failed");
                    callback.Invoke(LoadingResult.Success());
                }
                //Not critical if can't auth user...
            );
        }
    }
}