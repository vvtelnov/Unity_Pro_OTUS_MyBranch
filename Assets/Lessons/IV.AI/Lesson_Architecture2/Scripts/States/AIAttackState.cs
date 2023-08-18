using Entities;
using Game.GameEngine.Mechanics;
using UnityEngine;

namespace Lessons.AI.HierarchicalStateMachine
{
    public sealed class AIAttackState : AIState
    {
        [SerializeField]
        private Blackboard blackboard;

        [SerializeField]
        private AIMoveToPositionState moveToPositionState;

        [SerializeField]
        private AICombatState combatState;

        public override void OnEnter()
        {
            this.moveToPositionState.OnEnter();
        }

        public override void OnUpdate()
        {
            var target = this.blackboard.GetVariable<IEntity>(BlackboardKeys.ENEMY);
            var targetPosition = target.Get<IComponent_GetPosition>().Position;
            
            this.blackboard.SetVariable(BlackboardKeys.MOVE_POSITION, targetPosition);
            
            this.moveToPositionState.OnUpdate();

            if (this.moveToPositionState.IsReached)
            {
                this.combatState.OnEnter();
            }
            else
            {
                this.combatState.OnExit();
            }
        }

        public override void OnExit()
        {
            this.moveToPositionState.OnExit();
            this.combatState.OnExit();
        }
    }
}