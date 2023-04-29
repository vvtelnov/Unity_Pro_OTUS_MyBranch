namespace AI.GOAP
{
    public interface IGoal
    {
        IFactState ResultState { get; }

        bool IsValid();
        
        int EvaluatePriority();
    }
}