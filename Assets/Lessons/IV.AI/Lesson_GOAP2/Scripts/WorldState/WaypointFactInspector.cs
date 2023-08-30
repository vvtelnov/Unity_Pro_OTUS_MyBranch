using AI.GOAP;
using Entities;
using Game.GameEngine.Mechanics;
using Lessons.AI.HierarchicalStateMachine;
using UnityEngine;

namespace Lessons.AI.Lesson_GOAP2
{
    public sealed class WaypointFactInspector : FactInspector
    {
        [SerializeField]
        private Blackboard blackboard;

        [Space]
        [SerializeField, FactId]
        private string atWaypointKey;

        public override void OnUpdate(WorldState worldState)
        {
            if (this.blackboard.TryGetVariable(BlackboardKeys.UNIT, out IEntity unit) &&
                this.blackboard.TryGetVariable(BlackboardKeys.WAYPOINTS, out Transform[] waypoints) &&
                this.blackboard.TryGetVariable(BlackboardKeys.WAYPOINT_INDEX, out int waypointIndex) &&
                this.blackboard.TryGetVariable(BlackboardKeys.STOPPING_DISTANCE, out float stoppingDistance))
            {
                var unitPosition = unit.Get<IComponent_GetPosition>().Position;
                var waypointPosition = waypoints[waypointIndex].position;
                var atWaypoint = Vector3.Distance(unitPosition, waypointPosition) <= stoppingDistance;
                worldState.SetFact(this.atWaypointKey, atWaypoint);
            }
            else
            {
                worldState.RemoveFact(this.atWaypointKey);
            }
        }
    }
}