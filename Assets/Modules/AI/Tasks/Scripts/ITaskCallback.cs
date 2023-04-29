namespace AI.Tasks
{
    public interface ITaskCallback
    {
        void Invoke(ITask task, bool success);
    }
}