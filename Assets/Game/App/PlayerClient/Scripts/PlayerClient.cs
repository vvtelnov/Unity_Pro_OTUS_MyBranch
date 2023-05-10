using System;
using System.Threading.Tasks;
using Asyncoroutine;
using Services;
using static UnityEngine.Networking.UnityWebRequest.Result;

// ReSharper disable UnusedParameter.Local

namespace Game.App
{
    public sealed class PlayerClient
    {
        public bool IsAuthorized()
        {
            return this.UserId != null && this.Token != null;
        }

        public string UserId { get; set; }

        public string Token { get; set; }

        private BackendServer server;

        [ServiceInject]
        public void Construct(BackendServer server)
        {
            this.server = server;
        }

        public async Task<(bool, string)> DownloadState()
        {
            if (!this.IsAuthorized())
            {
                throw new Exception("Player client is not authorized!");
            }

            var route = $"load_player?userId={this.UserId}&token={this.Token}";
            using (var request = this.server.Get(route))
            {
                await request.SendWebRequest();

                if (request.result is ConnectionError or ProtocolError)
                {
                    return (false, null);
                }

                var playerState = request.downloadHandler.text;
                return (true, playerState);
            }
        }

        public async Task<bool> UploadState(string data)
        {
            if (!this.IsAuthorized())
            {
                throw new Exception("Player client is not authorized!");
            }

            var route = $"save_player?userId={this.UserId}&token={this.Token}";
            using (var request = this.server.Put(route, data))
            {
                await request.SendWebRequest();
                return request.result == Success;
            }
        }
    }
}