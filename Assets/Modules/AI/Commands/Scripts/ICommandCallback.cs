namespace AI.Commands
{
    public interface ICommandCallback
    {
        void Invoke(ICommand command, object args, bool success);
    }
}