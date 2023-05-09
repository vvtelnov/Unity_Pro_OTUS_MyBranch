using System;
using Services;

namespace Game.App
{
    public sealed class GameSaver :
        IGameStartListener,
        IGameStopListener
    {
        public event Action OnSaved;
        
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

        void IGameStartListener.OnStartGame(GameFacade gameFacade)
        {
            this.remainingSeconds = SAVE_PERIOD_IN_SECONDS;

            this.applicationManager.OnUpdate += this.OnApplicationUpdate;
            this.applicationManager.OnPaused += this.OnApplicationPaused;
            this.applicationManager.OnQuit += this.OnQuitApplication;
        }

        void IGameStopListener.OnStopGame(GameFacade gameFacade)
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

            this.OnSaved?.Invoke();
            this.remainingSeconds = SAVE_PERIOD_IN_SECONDS;
        }
    }
}