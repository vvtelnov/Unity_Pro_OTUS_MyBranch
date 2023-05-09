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
            this.userAuth.Authenticate(success =>
            {
#if UNITY_EDITOR
                Debug.Log($"Success auth: {success}");
#endif
                callback.Invoke(LoadingResult.Success()); //Not critical if can't auth user...
            });
        }
    }
}