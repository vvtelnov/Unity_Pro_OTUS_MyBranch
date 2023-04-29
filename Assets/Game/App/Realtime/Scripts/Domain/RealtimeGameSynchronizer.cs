using Game.GameEngine;
using Services;

namespace Game.App
{
    public sealed class RealtimeGameSynchronizer : 
        IAppStartListener,
        IAppQuitListener,
        IGameLoadDataListener
    {
        [ServiceInject]
        private RealtimeClock realtimeClock;

        private TimeShiftEmitter timeShiftEmitter;
        
        void IAppStartListener.Start()
        {
            this.realtimeClock.OnStarted += this.OnSessionStarted;
            this.realtimeClock.OnResumed += this.OnSessionResumed;
        }

        void IAppQuitListener.OnQuit()
        {
            this.realtimeClock.OnStarted -= this.OnSessionStarted;
            this.realtimeClock.OnResumed -= this.OnSessionResumed;
        }

        void IGameLoadDataListener.OnLoadData(GameContainer gameContainer)
        {
            this.timeShiftEmitter = gameContainer.GetService<TimeShiftEmitter>();
        }

        private void OnSessionStarted(long pauseSeconds)
        {
            if (pauseSeconds > 0)
            {
                this.timeShiftEmitter.EmitEvent(TimeShiftReason.START_GAME, pauseSeconds);
            }
        }

        private void OnSessionResumed(long pauseSeconds)
        {
            if (pauseSeconds > 0)
            {
                this.timeShiftEmitter.EmitEvent(TimeShiftReason.RESUME_GAME, pauseSeconds);
            }
        }
    }
}