namespace AI.GOAP
{
    public static class FactExtensions
    {
        public static bool EqualsTo(this IFactState target, IFactState other)
        {
            foreach (var (id, value) in target)
            {
                if (!other.TryGetFact(id, out var otherValue))
                {
                    return false;
                }

                if (value != otherValue)
                {
                    return false;
                }
            }

            return true;
        }
        
        public static bool IsNeighbourTo(this IFactState target, IFactState other)
        {
            foreach (var (id, _) in target)
            {
                if (other.ContainsFact(id))
                {
                    return true;
                }
            }

            return false;
        }
        
       
    }
}