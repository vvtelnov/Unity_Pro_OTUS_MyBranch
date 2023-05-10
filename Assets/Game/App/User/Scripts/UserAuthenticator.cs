using System;
using Services;
using UnityEngine;

// ReSharper disable NotAccessedField.Local

namespace Game.App
{
    public sealed class UserAuthenticator
    {
        private const string PREFS_KEY = "UserData";

        private BackendServer server;

        private PlayerClient client;

        [ServiceInject]
        public void Construct(BackendServer server, PlayerClient client)
        {
            this.server = server;
            this.client = client;
        }

        public void Authenticate(Action<bool> callback = null)
        {
            if (PlayerPreferences.TryLoad(PREFS_KEY, out UserData user))
            {
                this.SignIn(user.id, user.password, callback);
            }
            else
            {
                this.SignUp(callback);
            }
        }

        private async void SignIn(string userId, string password, Action<bool> callback)
        {
            var request = new SignInRequest
            {
                userId = userId,
                password = password
            };
            
            await this.server.RequestPost<SignInRequest, SignInResponse>("signIn", request,
                onSuccess: response =>
                {
                    this.client.UserId = userId;
                    this.client.Token = response.token;
                    callback?.Invoke(true);
                },
                onError: _ =>
                {
                    callback?.Invoke(false);
                });
        }
        
        public async void SignUp(Action<bool> callback)
        {
            var deviceId = SystemInfo.deviceUniqueIdentifier;
            
            var url = $"signUp/?deviceId={deviceId}";
            await this.server.RequestGet<SignUpResponse>(url,
                onSuccess : response =>
                {
                    this.client.UserId = response.userId;
                    this.client.Token = response.token;

                    PlayerPreferences.Save(PREFS_KEY, new UserData
                    {
                        id = response.userId,
                        password = response.password
                    });
                    
                    callback?.Invoke(true);
                },
                onError: _ =>
                {
                    callback?.Invoke(false);
                });
        }

        private struct SignInRequest
        {
            public string userId;
            public string password;
        }
        
        private struct SignInResponse
        {
            public string token;
        }

        private struct SignUpResponse
        {
            public string userId;
            public string password;
            public string token;
        }
    }
}