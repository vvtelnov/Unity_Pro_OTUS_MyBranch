using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AI.GOAP
{
    public sealed class DFSPlanner : IPlanner
    {
        private readonly IEnumerable<IActor> allActions;
        
        private IEnumerable<IActor> validActions;
        private IFactState worldState;

        public DFSPlanner(IEnumerable<IActor> allActions)
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

            return this.MakePlanRecursively(goal, baseNode: null, out plan);
        }

        private bool MakePlanRecursively(IFactState goal, Node baseNode, out List<IActor> plan)
        {
            var neighbours = this.FindNeighbours(goal);
            var orderedNeighbours = neighbours.OrderBy(it => it.EvaluateCost());

            foreach (var action in orderedNeighbours)
            {
                var node = new Node
                {
                    baseNode = baseNode,
                    action = action
                };
                
                var requiredState = action.RequiredState;
                if (requiredState.EqualsTo(this.worldState))
                {
                    plan = this.CreatePlan(node);
                    return true;
                }

                if (this.MakePlanRecursively(requiredState, node, out plan))
                {
                    return true;
                }
            }

            plan = null;
            return false;
        }

        private List<IActor> FindNeighbours(IFactState goal)
        {
            var result = new List<IActor>();
            
            foreach (var action in this.validActions)
            {
                if (this.MatchesAction(goal, action))
                {
                    result.Add(action);
                }
            }

            return result;
        }

        private bool MatchesAction(IFactState goal, IActor action)
        {
            foreach (var (conditionName, conditionValue) in goal)
            {
                if (this.worldState.TryGetFact(conditionName, out var worldValue) && 
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

        private List<IActor> CreatePlan(Node endNode)
        {
            var plan = new List<IActor>();

            while (endNode != null)
            {
                plan.Add(endNode.action);
                endNode = endNode.baseNode;
            }
            
            return plan;
        }

        private sealed class Node
        {
            public Node baseNode;
            public IActor action;
        }
    }
}
