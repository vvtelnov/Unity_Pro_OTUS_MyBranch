using System;
using System.Collections.Generic;
using Services;

namespace Game.App
{
    public sealed class PlayerClient
    {
        public bool IsDownloaded
        {
            get { return this.playerState != null; }
        }

        private BackendServer server;

        private UserAuthenticator userAuth;

        private Dictionary<string, object> playerState;

        [ServiceInject]
        public void Construct(BackendServer server, UserAuthenticator userAuth)
        {
            this.server = server;
            this.userAuth = userAuth;
        }

        public async void DownloadState(Action onSuccess, Action onError)
        {
            var url = $"load_player?userId={this.userAuth.Id}&token={this.userAuth.Token}";
            await this.server.RequestGet<Dictionary<string, object>>(url,
                onSuccess: playerState =>
                {
                    this.playerState = playerState;
                    onSuccess?.Invoke();
                },
                onError: _ => onError?.Invoke());
        }

        public object GetDownloadedRaw(string key)
        {
            if (this.playerState == null)
            {
                throw new Exception("Player state is not downloaded!");
            }

            if (!this.playerState.TryGetValue(key, out var value))
            {
                throw new Exception($"Key not found {key}!");
            }

            return value;
        }

        public Int64 GetDownloadedInt64(string key)
        {
            return (Int64) this.GetDownloadedRaw(key);
        }
    }
}