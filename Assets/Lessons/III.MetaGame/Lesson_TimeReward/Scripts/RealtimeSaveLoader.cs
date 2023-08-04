using System;
using System.Collections.Generic;
using System.Globalization;
using Game.App;
using UnityEngine;

namespace Lessons.MetaGame
{
    public sealed class RealtimeSaveLoader : MonoBehaviour, IGameLoadListener
    {
        private readonly HashSet<IRealtimeTimer> timers = new();

        public void RegisterTimer(IRealtimeTimer timer)
        {
            if (this.timers.Add(timer))
            {
                timer.OnStarted += this.SaveTimer;
            }
        }

        public void UnregisterTimer(IRealtimeTimer timer)
        {
            if (this.timers.Add(timer))
            {
                timer.OnStarted -= this.SaveTimer;
            }
        }

        public void OnLoadGame(GameFacade gameFacade)
        { 
            var now = DateTime.Now;
            foreach (var timer in timers)
            {
                this.SynchronizeTimer(timer, now);
            }
        }

        private void SynchronizeTimer(IRealtimeTimer timer, DateTime now)
        {
            var timerId = timer.Id;
            
            if (!PlayerPrefs.HasKey(timerId))
            {
                return;
            }

            var serializedTime = PlayerPrefs.GetString(timerId);
            var previousTime = DateTime.Parse(serializedTime, CultureInfo.InvariantCulture);

            var timeSpan = now - previousTime;
            var pauseSeconds = timeSpan.TotalSeconds;
            timer.Synchronize((float) pauseSeconds);
            Debug.Log($"PAUSE SECONDS {timer.Id} {pauseSeconds}");
        }

        private void SaveTimer(IRealtimeTimer timer)
        {
            Debug.Log($"SAVE TIMER {timer.Id}");

            var currentTime = DateTime.Now;
            var serializedTime = currentTime.ToString(CultureInfo.InvariantCulture);
            PlayerPrefs.SetString(timer.Id, serializedTime);
        }
    }


    // public sealed class TimeRewardSaveLoader : MonoBehaviour, IGameLoadListener
    // {
    //     private const string GAME_TIME_PREFS = "GameTime";
    //     private TimeReward timeReward;
    //
    //     public void OnLoadGame(GameFacade gameFacade)
    //     {
    //         this.timeReward = gameFacade.GetService<TimeReward>();
    //         this.timeReward.OnTimerStarted += this.OnTimerStarted;
    //         this.SynchronizeTime();
    //     }
    //
    //     private void SynchronizeTime()
    //     {
    //         if (!PlayerPrefs.HasKey(GAME_TIME_PREFS))
    //         {
    //             return;
    //         }
    //
    //         var serializedTime = PlayerPrefs.GetString(GAME_TIME_PREFS);
    //         var previousTime = DateTime.Parse(serializedTime, CultureInfo.InvariantCulture);
    //
    //         var timeSpan = DateTime.Now - previousTime;
    //         var pauseSeconds = timeSpan.TotalSeconds;
    //         this.timeReward.DecrementTimer((float) pauseSeconds);
    //         Debug.Log($"PAUSE SECONDS {pauseSeconds}");
    //     }
    //
    //     private void OnTimerStarted()
    //     {
    //         SaveTime();
    //     }
    //
    //     private void SaveTime()
    //     {
    //         Debug.Log("SAVE TIMER");
    //         var currentTime = DateTime.Now;
    //         var serializedTime = currentTime.ToString(CultureInfo.InvariantCulture);
    //         PlayerPrefs.SetString(GAME_TIME_PREFS, serializedTime);
    //     }
    // }

    // public sealed class TimeSynchronizer : IGameLoadListener
    // {
    //     private ITimeSynchronizable[] synchronizables;
    //     
    //     public void OnLoadGame(GameFacade facade)
    //     {
    //         var now = DateTime.Now;
    //         
    //         foreach (var synchronizable in this.synchronizables)
    //         {
    //             synchronizable.OnTimerStarted += this.OnTimeStarted;
    //             this.LoadTimer(synchronizable, now);
    //         }
    //     }
    //
    //     private void LoadTimer(ITimeSynchronizable synchronizable, DateTime now)
    //     {
    //         if (!PlayerPrefs.HasKey("GameTime/ " + synchronizable.TimerId))
    //         {
    //             return;
    //         }
    //
    //         var serializedTime = PlayerPrefs.GetString("GameTime/ " + synchronizable.TimerId);
    //         var previousTime = DateTime.Parse(serializedTime, CultureInfo.InvariantCulture);
    //
    //         var timeSpan = now - previousTime;
    //         var pauseSeconds = timeSpan.TotalSeconds;
    //         synchronizable.SyncTimer((float) pauseSeconds);
    //         Debug.Log($"PAUSE SECONDS {pauseSeconds}");
    //     }
    //
    //     private void OnTimeStarted(ITimeSynchronizable synchronizable)
    //     {
    //         Debug.Log("SAVE TIMER");
    //         var currentTime = DateTime.Now;
    //         var serializedTime = currentTime.ToString(CultureInfo.InvariantCulture);
    //         PlayerPrefs.SetString(synchronizable.TimerId, serializedTime);
    //     }
    // }
}