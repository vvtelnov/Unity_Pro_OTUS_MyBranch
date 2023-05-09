using System;
using Services;

namespace Game.App
{
    public sealed class PlayerDownloader
    {
        private BackendServer server;

        private PlayerClient client;

        private Action<bool> callback;

        [ServiceInject]
        public void Construct(BackendServer server, PlayerClient client)
        {
            this.server = server;
            this.client = client;
        }

        public async void DownloadState(Action<bool> callback = null)
        {
            if (!this.client.IsAuthorized)
            {
                callback?.Invoke(false);
                return;
            }

            this.callback = callback;

            var url = $"load_player?userId={this.client.UserId}&token={this.client.Token}";
            await this.server.RequestGet(url, this.OnSuccess, this.OnError);
        }

        private void OnSuccess(string playerState)
        {
            this.client.SetPlayerData(playerState);
            this.callback?.Invoke(true);
        }

        private void OnError(string error)
        {
            this.callback?.Invoke(false);
        }
    }
}