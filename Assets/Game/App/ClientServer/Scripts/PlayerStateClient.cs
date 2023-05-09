using System;
using System.Collections.Generic;

namespace Game.App
{
    public sealed class PlayerStateClient
    {
        private Server server;

        private UserAuthenticator userInfo;

        public async void LoadPlayer(Action<Dictionary<string, object>> onSuccess, Action onError)
        {
            var url = $"load_player?userId={this.userInfo.Id}&token={this.userInfo.Token}";
            await this.server.RequestGet(url, onSuccess, _ => onError?.Invoke());
        }

        public async void SavePlayer(Dictionary<string, object> data, Action onSuccess, Action onError)
        {
            var url = $"save_player?userId={this.userInfo.Id}&token={this.userInfo.Token}";
            await this.server.RequestPut(url, data, onSuccess, _ => onError?.Invoke());
        }
    }
}