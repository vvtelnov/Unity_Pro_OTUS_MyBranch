using Lessons.AI.HierarchicalStateMachine;
using UnityEngine;
using Blackboard = Lessons.AI.HierarchicalStateMachine.Blackboard;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BehaviourNode_AssignWaypoint : BehaviourNode
    {
        [SerializeField]
        private Blackboard blackboard;

        protected override void Run()
        {
            if (!this.blackboard.TryGetVariable(BlackboardKeys.WAYPOINTS, out Transform[] waypoints))
            {
                this.Return(false);
                return;
            }

            if (!this.blackboard.TryGetVariable(BlackboardKeys.WAYPOINT_INDEX, out int index))
            {
                this.Return(false);
                return;
            }

            Vector3 targetPosition = waypoints[index].position;
            this.blackboard.SetVariable(BlackboardKeys.MOVE_POSITION, targetPosition);
            this.Return(true);
        }
    }
}