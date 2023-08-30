using System.Collections.Generic;
using System.Linq;

namespace AI.GOAP
{
    public sealed class GreedyPlanner : IPlanner
    {
        private readonly IEnumerable<IActor> allActions;
        private IEnumerable<IActor> validActions;
        private IFactState worldState;

        public GreedyPlanner(IEnumerable<IActor> allActions)
        {
            this.allActions = allActions;
        }

        public bool MakePlan(IFactState worldState, IFactState goal, out List<IActor> plan)
        {
            if (goal.EqualsTo(worldState))
            {
                plan = new List<IActor>();
                return true;
            }

            this.validActions = this.allActions.Where(it => it.IsValid());
            this.worldState = worldState;

            plan = new List<IActor>();

            while (this.FindNextAction(goal, out var nextAction))
            {
                plan.Add(nextAction);
                
                goal = nextAction.RequiredState;
                
                if (goal.EqualsTo(worldState))
                {
                    plan.Reverse();
                    return true;
                }
            }

            plan = default;
            return false;
        }

        private bool FindNextAction(IFactState requiredState, out IActor cheapestAction)
        {
            cheapestAction = null;
            var currentCost = int.MaxValue;

            foreach (var action in this.validActions)
            {
                if (!PlannerUtils.MatchesAction(action, requiredState, this.worldState))
                {
                    continue;
                }

                var cost = action.EvaluateCost();
                
                if (cheapestAction == null || currentCost > cost)
                {
                    cheapestAction = action;
                    currentCost = cost;
                }
            }

            return cheapestAction != null;
        }
    }
}