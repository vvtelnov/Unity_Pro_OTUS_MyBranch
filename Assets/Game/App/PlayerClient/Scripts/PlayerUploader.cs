using System.Threading.Tasks;
using Newtonsoft.Json;
using Services;
using UnityEngine;

// ReSharper disable NotAccessedField.Local

namespace Game.App
{
    public sealed class PlayerUploader : IGameStartListener, IGameStopListener
    {
        private BackendServer server;

        private PlayerClient client;

        private GameSaver gameSaver;

        private RealtimeClock realtimeClock;

        private bool isUploading;

        [ServiceInject]
        public void Construct(
            BackendServer server,
            PlayerClient client,
            GameSaver gameSaver,
            RealtimeClock realtimeClock
        )
        {
            this.server = server;
            this.client = client;
            this.gameSaver = gameSaver;
            this.realtimeClock = realtimeClock;
        }

        void IGameStartListener.OnStartGame(GameFacade gameFacade)
        {
            this.gameSaver.OnSaved += this.OnGameSaved;
        }

        void IGameStopListener.OnStopGame(GameFacade gameFacade)
        {
            this.gameSaver.OnSaved -= this.OnGameSaved;
        }

        private async void OnGameSaved()
        {
            if (!this.client.IsAuthorized())
            {
                return;
            }

            if (this.isUploading)
            {
                return;
            }

            this.isUploading = true;
            await UploadState();
            this.isUploading = false;
        }

        private async Task UploadState()
        {
            this.client.LastTime = this.realtimeClock.RealtimeSeconds;

            var url = $"save_player?userId={this.client.UserId}&token={this.client.Token}";
            var request = new PlayerRequest
            {
                userId = this.client.UserId,
                lastTime = this.client.LastTime,
                data = JsonConvert.SerializeObject(this.client.GetPlayerState()) 
            };

            Debug.Log($"Upload data {request.data}!");
            await this.server.RequestPut<PlayerRequest>(url, request, null, null);
        }

        private struct PlayerRequest
        {
            public string userId;
            public long lastTime;
            public string data;
        }
    }
}