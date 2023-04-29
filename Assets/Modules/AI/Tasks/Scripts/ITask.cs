namespace AI.Tasks
{
    public interface ITask
    {
        public bool IsPlaying { get; }

        public void Do(ITaskCallback callback);

        public void Cancel();
    }
}