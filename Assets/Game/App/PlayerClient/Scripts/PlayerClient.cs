using System;
using Services;

namespace Game.App
{
    public sealed class PlayerClient
    {
        private BackendServer server;

        private UserAuthenticator userAuth;

        [ServiceInject]
        public void Construct(BackendServer server, UserAuthenticator userAuth)
        {
            this.server = server;
            this.userAuth = userAuth;
        }

        public async void LoadPlayerState(Action<string> onSuccess, Action onError)
        {
            var url = $"load_player?userId={this.userAuth.Id}&token={this.userAuth.Token}";
            await this.server.RequestGet(url, onSuccess, _ => onError?.Invoke());
        }

        public async void SavePlayerState(string data, Action onSuccess, Action onError)
        {
            var url = $"save_player?userId={this.userAuth.Id}&token={this.userAuth.Token}";
            await this.server.RequestPut(url, data, onSuccess, _ => onError?.Invoke());
        }
    }
}