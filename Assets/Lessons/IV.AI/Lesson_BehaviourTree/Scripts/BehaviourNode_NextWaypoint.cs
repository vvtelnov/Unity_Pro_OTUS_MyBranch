using Lessons.AI.HierarchicalStateMachine;
using UnityEngine;
using Blackboard = Lessons.AI.HierarchicalStateMachine.Blackboard;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BehaviourNode_NextWaypoint : BehaviourNode
    {
        [SerializeField]
        private Blackboard blackboard;

        protected override void Run()
        {
            if (!this.blackboard.TryGetVariable(BlackboardKeys.WAYPOINT_INDEX, out int waypointIndex))
            {
                this.Return(false);
                return;
            }

            if (!this.blackboard.TryGetVariable(BlackboardKeys.WAYPOINTS, out Transform[] waypoints))
            {
                this.Return(false);
                return;
            }

            waypointIndex++;
            waypointIndex %= waypoints.Length;
            
            this.blackboard.SetVariable(BlackboardKeys.WAYPOINT_INDEX, waypointIndex);
            
            this.Return(true);
        }
    }
}