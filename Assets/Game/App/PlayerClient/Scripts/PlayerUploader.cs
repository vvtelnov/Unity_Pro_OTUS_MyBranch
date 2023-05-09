using Services;

namespace Game.App
{
    public sealed class PlayerUploader : IGameStartListener, IGameStopListener
    {
        private BackendServer server;

        private PlayerClient client;

        private GameSaver gameSaver;

        [ServiceInject]
        public void Construct(BackendServer server, PlayerClient client, GameSaver gameSaver)
        {
            this.server = server;
            this.client = client;
            this.gameSaver = gameSaver;
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
            if (!this.client.IsAuthorized)
            {
                return;
            }
            
            var url = $"save_player?userId={this.client.UserId}&token={this.client.Token}";
            var playerData = this.client.GetPlayerData();
            
            await this.server.RequestPut(url, playerData, null, null);
        }
    }
}