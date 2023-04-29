namespace AI.Commands
{
    public interface ICommand
    {
        bool IsPlaying { get; }

        void Execute(object args, ICommandCallback callback);

        void Interrupt();
    }
}