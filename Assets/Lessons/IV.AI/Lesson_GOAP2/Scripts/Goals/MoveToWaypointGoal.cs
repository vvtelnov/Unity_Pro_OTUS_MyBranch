using AI.GOAP;
using Lessons.AI.HierarchicalStateMachine;
using UnityEngine;

namespace Lessons.AI.Lesson_GOAP2
{
    public sealed class MoveToWaypointGoal : Goal
    {
        [SerializeField]
        private Blackboard blackboard;

        [SerializeField, Space]
        private int priority;

        public override bool IsValid()
        {
            return this.blackboard.HasVariable(BlackboardKeys.UNIT) &&
                   this.blackboard.HasVariable(BlackboardKeys.WAYPOINTS) &&
                   this.blackboard.HasVariable(BlackboardKeys.WAYPOINT_INDEX) &&
                   this.blackboard.HasVariable(BlackboardKeys.STOPPING_DISTANCE);
        }

        public override int EvaluatePriority()
        {
            return this.priority;
        }
    }
}