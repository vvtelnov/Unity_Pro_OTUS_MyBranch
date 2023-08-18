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
                this.StartAttack(target);
            }
            else
            {
                this.StopAttack();
            }
        }

        public override void OnExit()
        {
            this.moveToPositionState.OnExit();
            this.StopAttack();
        }

        private void StartAttack(IEntity target)
        {
            var unit = this.blackboard.GetVariable<IEntity>(BlackboardKeys.UNIT);
            var combatComponent = unit.Get<IComponent_MeleeCombat>();
            if (!combatComponent.IsCombat)
            {
                combatComponent.StartCombat(new CombatOperation(target));
            }
        }

        private void StopAttack()
        {
            var unit = this.blackboard.GetVariable<IEntity>(BlackboardKeys.UNIT);
            var combatComponent = unit.Get<IComponent_MeleeCombat>();
            if (combatComponent.IsCombat)
            {
                combatComponent.StopCombat();
            }
        }
    }
}