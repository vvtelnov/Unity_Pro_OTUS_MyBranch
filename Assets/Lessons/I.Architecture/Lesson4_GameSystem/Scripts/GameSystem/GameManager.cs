using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Architecture.GameSystem
{
    public enum GameState
    {
        OFF = 0,
        PLAYED = 1,
        PAUSED = 2,
        FINISHED = 3
    }

    public sealed class GameManager : MonoBehaviour
    {
        [ShowInInspector, ReadOnly]
        public GameState State { get; private set; }

        private readonly List<IGameListener> listeners = new();

        private readonly List<IGameUpdateListener> updateListeners = new();

        private readonly List<IGameFixedUpdateListener> fixedUpdateListeners = new();

        private readonly List<IGameLateUpdateListener> lateUpdateListeners = new();

        private void Update()
        {
            if (this.State != GameState.PLAYED)
            {
                return;
            }

            var deltaTime = Time.deltaTime;
            foreach (var listener in this.updateListeners)
            {
                listener.OnUpdate(deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (this.State != GameState.PLAYED)
            {
                return;
            }

            var deltaTime = Time.fixedDeltaTime;
            foreach (var listener in this.fixedUpdateListeners)
            {
                listener.OnFixedUpdate(deltaTime);
            }
        }

        private void LateUpdate()
        {
            if (this.State != GameState.PLAYED)
            {
                return;
            }

            var deltaTime = Time.deltaTime;
            foreach (var listener in this.lateUpdateListeners)
            {
                listener.OnLateUpdate(deltaTime);
            }
        }


        public void AddListener(IGameListener listener)
        {
            this.listeners.Add(listener);

            if (listener is IGameUpdateListener updateListener)
            {
                this.updateListeners.Add(updateListener);
            }

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
            {
                this.fixedUpdateListeners.Add(fixedUpdateListener);
            }

            if (listener is IGameLateUpdateListener lateUpdateListener)
            {
                this.lateUpdateListeners.Add(lateUpdateListener);
            }
        }

        public void RemoveListener(IGameListener listener)
        {
            this.listeners.Remove(listener);
            
            if (listener is IGameUpdateListener updateListener)
            {
                this.updateListeners.Remove(updateListener);
            }

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
            {
                this.fixedUpdateListeners.Remove(fixedUpdateListener);
            }

            if (listener is IGameLateUpdateListener lateUpdateListener)
            {
                this.lateUpdateListeners.Remove(lateUpdateListener);
            }
        }

        [Button]
        public void StartGame()
        {
            foreach (var listener in this.listeners)
            {
                if (listener is IGameStartListener startListener)
                {
                    startListener.OnStartGame();
                }
            }

            this.State = GameState.PLAYED;
        }

        [Button]
        public void PauseGame()
        {
            foreach (var listener in this.listeners)
            {
                if (listener is IGamePauseListener pauseListener)
                {
                    pauseListener.OnPauseGame();
                }
            }

            this.State = GameState.PAUSED;
        }

        [Button]
        public void ResumeGame()
        {
            foreach (var listener in this.listeners)
            {
                if (listener is IGameResumeListener resumeListener)
                {
                    resumeListener.OnResumeGame();
                }
            }

            this.State = GameState.PLAYED;
        }

        [Button]
        public void FinishGame()
        {
            foreach (var listener in this.listeners)
            {
                if (listener is IGameFinishListener finishListener)
                {
                    finishListener.OnFinishGame();
                }
            }

            this.State = GameState.FINISHED;
        }
    }
}