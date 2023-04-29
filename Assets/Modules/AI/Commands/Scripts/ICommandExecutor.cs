using System;

namespace AI.Commands
{
    public interface ICommandExecutor<T>
    {
        event Action<T, object> OnStarted;

        event Action<T, object> OnFinished;

        event Action<T, object> OnInterrupted;

        bool IsRunning { get; }

        void Execute(T key, object args = null);

        void Interrupt();

        bool TryGetRunningInfo(out T key, out object args);

        void RegisterCommand(T key, ICommand command);

        void UnregisterCommand(T key);
    }
}