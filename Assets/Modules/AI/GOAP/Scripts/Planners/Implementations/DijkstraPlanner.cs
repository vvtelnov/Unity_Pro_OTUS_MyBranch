using System.Collections.Generic;

namespace AI.GOAP
{
    public sealed class DijkstraPlanner : IPlanner
    {
        private IFactState worldState;
        private IFactState goal;
        private IActor[] actions;

        public bool MakePlan(IFactState worldState, IFactState goal, IActor[] actions, out List<IActor> plan)
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
            this.actions = actions;
            
            return this.MakePlan(out plan);
        }

        private bool MakePlan(out List<IActor> plan)
        {
            var graph = new Graph(this.actions);

            var distances = new Dictionary<IActor, int>();
            var comparer = new DistanceComparer(distances);

            var sequence = new Dictionary<IActor, IActor>();
            var queue = new List<IActor>(this.actions);

            // Initialize distances:
            foreach (var action in this.actions)
            {
                if (action.RequiredState.EqualsTo(this.worldState))
                {
                    distances[action] = action.EvaluateCost();
                }
                else
                {
                    distances[action] = int.MaxValue;
                }
            }

            while (queue.Count > 0)
            {
                // Get node with minimum distance:
                queue.Sort(comparer);
                var current = queue[0];
                queue.RemoveAt(0);

                // Check if reached goal:
                if (current.ResultState.EqualsTo(this.goal))
                {
                    // Construct plan:
                    plan = new List<IActor> {current};
                    while (sequence.TryGetValue(current, out current))
                    {
                        plan.Add(current);
                    }

                    plan.Reverse();
                    return true;
                }

                // Update distances and sequence:
                foreach (var neighbor in graph.GetNeighbours(current))
                {
                    var cost = distances[current] + neighbor.EvaluateCost();
                    if (cost < distances[neighbor])
                    {
                        distances[neighbor] = cost;
                        sequence[neighbor] = current;
                    }
                }
            }

            //Plan not found:
            plan = default;
            return false;
        }

        private sealed class DistanceComparer : IComparer<IActor>
        {
            private readonly Dictionary<IActor, int> distances;

            public DistanceComparer(Dictionary<IActor, int> distances)
            {
                this.distances = distances;
            }

            public int Compare(IActor a, IActor b)
            {
                return this.distances[a!] - this.distances[b!];
            }
        }

        private sealed class Graph
        {
            private readonly Dictionary<IActor, List<IActor>> graph;

            public Graph(IActor[] actions)
            {
                this.graph = new Dictionary<IActor, List<IActor>>();
                var count = actions.Length;

                for (var i = 0; i < count; i++)
                {
                    var current = actions[i];
                    var currentState = current.ResultState;

                    for (var j = 0; j < count; j++)
                    {
                        var other = actions[j];
                        if (other == current)
                        {
                            continue;
                        }

                        if (!currentState.IsNeighbourTo(other.RequiredState))
                        {
                            continue;
                        }

                        if (!this.graph.TryGetValue(current, out var neighbours))
                        {
                            neighbours = new List<IActor>();
                            this.graph.Add(current, neighbours);
                        }

                        neighbours.Add(other);
                    }
                }
            }

            public List<IActor> GetNeighbours(IActor action)
            {
                if (this.graph.TryGetValue(action, out var neighbours))
                {
                    return neighbours;
                }

                neighbours = new List<IActor>(0);
                this.graph.Add(action, neighbours);
                return neighbours;
            }
        }
    }
}