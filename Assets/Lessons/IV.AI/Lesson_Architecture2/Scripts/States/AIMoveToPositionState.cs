using Entities;
using Game.GameEngine.Mechanics;
using UnityEngine;

namespace Lessons.AI.HierarchicalStateMachine
{
    public sealed class AIMoveToPositionState : AIState
    {
        [SerializeField]
        private Blackboard blackboard;
        
        private bool isReached;

        public bool IsReached
        {
            get { return this.isReached; }
        }

        public override void OnUpdate()
        {
            if (!this.blackboard.HasVariable(BlackboardKeys.MOVE_POSITION) ||
                !this.blackboard.HasVariable(BlackboardKeys.STOPPING_DISTANCE))
            {
                return;
            }

            var unit = this.blackboard.GetVariable<IEntity>(BlackboardKeys.UNIT);
            var myPosition = unit.Get<IComponent_GetPosition>().Position;
            var targetPosition = this.blackboard.GetVariable<Vector3>(BlackboardKeys.MOVE_POSITION);

            var distanceVector = targetPosition - myPosition;
            var stoppingDistance = this.blackboard.GetVariable<float>(BlackboardKeys.STOPPING_DISTANCE);

            this.isReached = distanceVector.magnitude <= stoppingDistance;
            if (!this.isReached)
            {
                var moveDirection = distanceVector.normalized;
                unit.Get<IComponent_MoveInDirection>().Move(moveDirection);
            }
        }
    }
}