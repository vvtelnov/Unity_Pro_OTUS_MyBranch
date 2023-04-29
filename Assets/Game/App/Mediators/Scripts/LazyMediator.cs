using Services;

namespace Game.App
{
    public abstract class LazyMediator<TRepository, TGameService> :
        IGameLoadDataListener,
        IGameSaveDataListener,
        IGameStartListener,
        IGameStopListener
    {
        private bool saveRequired;

        private TRepository repository;

        private TGameService gameService;
        
        void IGameLoadDataListener.OnLoadData(GameContainer gameContainer)
        {
            this.repository = ServiceLocator.GetService<TRepository>();
            this.gameService = gameContainer.GetService<TGameService>();
            this.OnLoadData(this.repository, this.gameService);
        }

        void IGameSaveDataListener.OnSaveData(GameSaveReason reason)
        {
            if (this.saveRequired)
            {
                this.OnSaveData(this.repository, this.gameService);
                this.saveRequired = false;
            }
        }

        void IGameStartListener.OnStartGame(GameContainer gameContainer)
        {
            this.OnStartGame(this.gameService);
        }

        void IGameStopListener.OnStopGame(GameContainer gameContainer)
        {
            this.OnStopGame(this.gameService);
        }

        protected abstract void OnLoadData(TRepository repository, TGameService gameService);

        protected abstract void OnSaveData(TRepository repository, TGameService gameService);

        protected abstract void OnStartGame(TGameService gameService);

        protected abstract void OnStopGame(TGameService gameService);

        protected void MarkSaveRequired()
        {
            this.saveRequired = true;
        }
    }
}