using System.Collections.Generic;
using Game.GameEngine.Mechanics;
using UnityEngine;
using static Lessons.AI.HierarchicalStateMachine.BlackboardKeys;

namespace Lessons.AI.HierarchicalStateMachine
{
    public sealed class AIPatrolWaypointsState : AIState
    {
        [SerializeField]
        private Blackboard blackboard;

        [SerializeField]
        private AIMoveToPositionState moveToPositionState;

        private IComponent_GetPosition positionComponent;

        private float pauseTime;

        public override void OnEnter()
        {
            this.moveToPositionState.OnEnter();
        }

        public override void OnUpdate()
        {
            if (this.pauseTime > 0)
            {
                this.pauseTime -= Time.fixedDeltaTime;
                return;
            }

            this.Patrol();
        }

        public override void OnExit()
        {
            this.moveToPositionState.OnExit();
            this.blackboard.RemoveVariable(MOVE_POSITION);
        }

        private void Patrol()
        {
            var waypoints = this.blackboard.GetVariable<IEnumerator<Vector3>>(WAYPOINTS);
            var targetPosition = waypoints.Current;
            this.blackboard.SetVariable(MOVE_POSITION, targetPosition);
            
            this.moveToPositionState.OnUpdate();
            
            if (this.moveToPositionState.IsReached)
            {
                waypoints.MoveNext();
                this.pauseTime = this.blackboard.GetVariable<float>(PATROL_PAUSE);
            }
        }
    }
}