namespace AI.GOAP
{
    public static class PlannerExtensions
    {
        public static bool MatchesAction(IFactState goal, IFactState worldState, IActor action)
        {
            foreach (var (conditionName, conditionValue) in goal)
            {
                if (worldState.TryGetFact(conditionName, out var worldValue) && 
                    worldValue == conditionValue)
                {
                    continue;
                }

                if (!action.ResultState.TryGetFact(conditionName, out var actionValue) ||
                    actionValue != conditionValue)
                {
                    return false;
                }
            }

            return true;
        }
    }
}