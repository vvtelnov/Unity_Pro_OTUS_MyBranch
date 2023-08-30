using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// ReSharper disable MemberCanBePrivate.Local

namespace AI.GOAP
{
    public sealed class AStarPlanner : IPlanner
    {
        ///Change consts for your planner
        private const int DISTANCE_BETWEEN_FACTS = 1;
        private const int HEURISTIC_BETWEEN_FACTS = 3;
        private const float HEURISTIC_COEFFICIENT = 1.0f;
        
        private static readonly CostComparer costComparer = new();

        private readonly IEnumerable<IActor> allActions;
        
        private IEnumerable<IActor> validActions;
        private Dictionary<IActor, Node> openList;
        private HashSet<IActor> closedList;

        private IFactState worldState;
        private IFactState goal;

        public AStarPlanner(IEnumerable<IActor> allActions)
        {
            this.allActions = allActions;
        }

        public bool MakePlan(IFactState worldState, IFactState goal, out List<IActor> plan)
        {
            if (goal == null)
            {
                plan = default;
                return false;
            }

            if (goal.EqualsTo(worldState))
            {
                plan = new List<IActor>(0);
                return true;
            }

            this.worldState = worldState;
            this.goal = goal;
            this.validActions = this.allActions.Where(it => it.IsValid());
            this.openList = new Dictionary<IActor, Node>();
            this.closedList = new HashSet<IActor>();

            return this.MakePlan(out plan);
        }

        private bool MakePlan(out List<IActor> plan)
        {
            this.ProcessStartActions();

            if (this.FindFinish(out var endNode))
            {
                plan = new List<IActor> {endNode.action};
                return true;
            }
            
            return this.MakePlanByGraph(out plan);
        }
        
        private bool MakePlanByGraph(out List<IActor> plan)
        {
            var graph = new ActionGraph(this.validActions.ToArray());

            while (this.SelectNextAction(out var node))
            {
                Debug.Log($"SELECTED ACTION {node.action}");
                
                this.closedList.Add(node.action);
                this.ProcessNeighbourActions(graph, node);

                if (this.FindFinish(out var endNode))
                {
                    plan = this.CreatePlan(endNode);
                    return true;
                }
                
                this.openList.Remove(node.action);
            }
            
            plan = default;
            return false;
        }

        private void ProcessStartActions()
        {
            foreach (var action in this.validActions)
            {
                if (this.goal.EqualsTo(action.ResultState))
                {
                    this.ProcessAction(action, null);
                }
            }
        }

        private void ProcessNeighbourActions(ActionGraph graph, Node node)
        {
            if (!graph.TryGetNeighbours(node.action, out var neighbours))
            {
                Debug.Log($"NO NEIGHBOURS FOR {node.action}");
                return;
            }

            foreach (var action in neighbours)
            {
                this.ProcessAction(action, node);
            }
        }

        private void ProcessAction(IActor action, Node baseNode)
        {
            if (this.closedList.Contains(action))
            {
                return;
            }

            var actionCost = action.EvaluateCost();
            var pathCost = baseNode?.cost + actionCost ?? actionCost;

            if (this.openList.TryGetValue(action, out var node))
            {
                if (node.cost > pathCost)
                {
                    node.baseNode = baseNode;
                    node.cost = pathCost;
                }
            }
            else
            {
                var heuristic = this.HeuristicDistance(action);
                node = new Node(action, baseNode, pathCost, heuristic);
                this.openList.Add(action, node);
            }
            
            Debug.Log($"NODE {node.action} C: {node.cost}, H: {node.heuristic} WEIGHT: {node.EvaluateWeight()}");
        }

        private bool FindFinish(out Node result)
        {
            var endActions = new List<Node>();
            foreach (var (action, node) in this.openList)
            {
                if (action.RequiredState.EqualsTo(this.worldState))
                {
                    endActions.Add(node);
                }
            }

            if (endActions.Count <= 0)
            {
                result = default;
                return false;
            }

            endActions.Sort(costComparer);

            result = endActions[0];
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

        private bool SelectNextAction(out Node result)
        {
            result = null;
            var minWeight = int.MaxValue;
            
            foreach (var node in this.openList.Values)
            {
                var weight = node.EvaluateWeight();
                if (result == null || minWeight > weight)
                {
                    result = node;
                    minWeight = weight;
                }
            }

            return result != null;
        }

        private int HeuristicDistance(IActor action)
        {
            var result = 0;

            foreach (var (requiredCond, requiredValue) in action.RequiredState)
            {
                if (this.worldState.TryGetFact(requiredCond, out var currentValue))
                {
                    if (requiredValue != currentValue)
                    {
                        result += DISTANCE_BETWEEN_FACTS;
                    }
                }
                else
                {
                    result += HEURISTIC_BETWEEN_FACTS;
                }
            }

            return Mathf.RoundToInt(result * HEURISTIC_COEFFICIENT);
        }
        
        private sealed class Node
        {
            public readonly IActor action;
            public readonly int heuristic;

            public Node baseNode;
            public int cost;

            public Node(IActor action, Node baseNode, int cost, int heuristic)
            {
                this.action = action;
                this.baseNode = baseNode;
                this.cost = cost;
                this.heuristic = heuristic;
            }

            public int EvaluateWeight()
            {
                return this.cost + this.heuristic;
            }
        }

        private sealed class CostComparer : IComparer<Node>
        {
            public int Compare(Node x, Node y)
            {
                return x.cost.CompareTo(y.cost);
            }
        }
        
        private sealed class ActionGraph
        {
            private readonly Dictionary<IActor, List<IActor>> graph;

            public ActionGraph(IActor[] actions)
            {
                this.graph = new Dictionary<IActor, List<IActor>>();
                
                var count = actions.Length;
                for (var i = 0; i < count; i++)
                {
                    var action = actions[i];

                    for (var j = 0; j < count; j++)
                    {
                        var other = actions[j];
                        if (other == action)
                        {
                            continue;
                        }

                        if (!action.RequiredState.IsNeighbourTo(other.ResultState))
                        {
                            continue;
                        }

                        if (!this.graph.TryGetValue(action, out var neighbours))
                        {
                            neighbours = new List<IActor>();
                            this.graph.Add(action, neighbours);
                        }

                        neighbours.Add(other);
                    }
                }
            }
            
            public bool TryGetNeighbours(IActor action, out List<IActor> neighbours)
            {
                return this.graph.TryGetValue(action, out neighbours);
            }
        }
    }
}