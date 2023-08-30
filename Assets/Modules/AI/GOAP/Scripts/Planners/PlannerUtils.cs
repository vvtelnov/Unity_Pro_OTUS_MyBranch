namespace AI.GOAP
{
    public static class PlannerUtils
    {
        public static bool MatchesAction(IActor action, IFactState goal, IFactState worldState)
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

        public static int HeuristicDistance(IActor action, IFactState worldState, int defaultDistance)
        {
            return PlannerUtils.HeuristicDistance(action.RequiredState, worldState, defaultDistance);
        }

        public static int HeuristicDistance(IFactState requiredState, IFactState worldState, int defaultDistance)
        {
            var result = 0;

            foreach (var (requiredCond, requiredValue) in requiredState)
            {
                if (worldState.TryGetFact(requiredCond, out var currentValue))
                {
                    if (requiredValue != currentValue)
                    {
                        result++;
                    }
                }
                else
                {
                    result += defaultDistance;
                }
            }

            return result;
        }
    }
}