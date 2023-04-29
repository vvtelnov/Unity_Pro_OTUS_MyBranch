using Services;

namespace Game.App
{
    public abstract class BaseMediator<TRepository, TGameService> :
        IGameLoadDataListener,
        IGameSaveDataListener
    {
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
            this.OnSaveData(this.repository, this.gameService);
        }

        protected abstract void OnLoadData(TRepository repository, TGameService gameService);

        protected abstract void OnSaveData(TRepository repository, TGameService gameService);
    }
}