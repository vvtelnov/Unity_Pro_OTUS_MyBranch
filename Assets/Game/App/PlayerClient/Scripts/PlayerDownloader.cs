using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Services;
using UnityEngine;

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
            if (!this.client.IsAuthorized())
            {
                callback?.Invoke(false);
                return;
            }

            this.callback = callback;

            var url = $"load_player?userId={this.client.UserId}&token={this.client.Token}";
            await this.server.RequestGet<PlayerResponse>(url, this.OnSuccess, this.OnError);
        }

        private void OnSuccess(PlayerResponse response)
        {
            Debug.Log($"Downloaded data: {response.lastTime} {response.data}");
            this.client.LastTime = response.lastTime;

            var playerData = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.data);
            if (playerData != null)
            {
                this.client.SetPlayerState(playerData);
            }

            this.callback?.Invoke(true);
        }

        private void OnError(string error)
        {
            this.callback?.Invoke(false);
        }

        private struct PlayerResponse
        {
            public string userId;
            public long lastTime;
            public string data;
        }
    }
}