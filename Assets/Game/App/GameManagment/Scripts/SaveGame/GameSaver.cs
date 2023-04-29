using Services;

namespace Game.App
{
    public sealed class GameSaver :
        IGameStartListener,
        IGameStopListener
    {
        private const float SAVE_PERIOD_IN_SECONDS = 30;

        private ApplicationManager applicationManager;
        
        private IGameSaveDataListener[] listeners;

        private float remainingSeconds;

        [ServiceInject]
        public void Construct(ApplicationManager applicationManager, IGameSaveDataListener[] listeners)
        {
            this.applicationManager = applicationManager;
            this.listeners = listeners;
        }

        void IGameStartListener.OnStartGame(GameContainer gameContainer)
        {
            this.remainingSeconds = SAVE_PERIOD_IN_SECONDS;

            this.applicationManager.OnUpdate += this.OnApplicationUpdate;
            this.applicationManager.OnPaused += this.OnApplicationPaused;
            this.applicationManager.OnQuit += this.OnQuitApplication;
        }

        void IGameStopListener.OnStopGame(GameContainer gameContainer)
        {
            this.applicationManager.OnUpdate -= this.OnApplicationUpdate;
            this.applicationManager.OnPaused -= this.OnApplicationPaused;
            this.applicationManager.OnQuit -= this.OnQuitApplication;
        }

        private void OnApplicationUpdate(float deltaTime)
        {
            this.remainingSeconds -= deltaTime;
            if (this.remainingSeconds <= 0.0f)
            {
                this.NotifyAboutSave(GameSaveReason.TIMER);
            }
        }

        private void OnApplicationPaused()
        {
            this.NotifyAboutSave(GameSaveReason.PAUSE);
        }

        private void OnQuitApplication()
        {
            this.NotifyAboutSave(GameSaveReason.QUIT);
        }

        private void NotifyAboutSave(GameSaveReason reason)
        {
            for (int i = 0, count = this.listeners.Length; i < count; i++)
            {
                var listener = this.listeners[i];
                listener.OnSaveData(reason);
            }

            this.remainingSeconds = SAVE_PERIOD_IN_SECONDS;
        }
    }
}