using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AI.Commands
{
    public abstract class UnityCommandExecutor<T> : MonoBehaviour, ICommandExecutor<T>
    {
        public event Action<T, object> OnStarted
        {
            add { this.executor.OnStarted += value; }
            remove { this.executor.OnStarted -= value; }
        }

        public event Action<T, object> OnFinished
        {
            add { this.executor.OnFinished += value; }
            remove { this.executor.OnFinished -= value; }
        }

        public event Action<T, object> OnInterrupted
        {
            add { this.executor.OnInterrupted += value; }
            remove { this.executor.OnInterrupted -= value; }
        }

        [ShowInInspector, ReadOnly]
        public bool IsRunning
        {
            get { return this.executor.IsRunning; }
        }

        protected readonly CommandExecutor<T> executor = new();

        [Button]
        public void Execute(T key, object args = null)
        {
            this.executor.Execute(key, args);
        }

        [Button]
        public void Interrupt()
        {
            this.executor.Interrupt();
        }

        public bool TryGetRunningInfo(out T key, out object args)
        {
            return this.executor.TryGetRunningInfo(out key, out args);
        }

        public void RegisterCommand(T key, ICommand command)
        {
            this.executor.RegisterCommand(key, command);
        }

        public void UnregisterCommand(T key)
        {
            this.executor.UnregisterCommand(key);
        }
    }
}