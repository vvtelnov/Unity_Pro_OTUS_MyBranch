using Entities;
using Game.GameEngine.Mechanics;
using UnityEngine;

namespace Lessons.AI.HierarchicalStateMachine
{
    public sealed class AICombatState : AIState
    {
        [SerializeField]
        private Blackboard blackboard;
        
        public override void OnEnter()
        {
            var unit = this.blackboard.GetVariable<IEntity>(BlackboardKeys.UNIT);
            var target = this.blackboard.GetVariable<IEntity>(BlackboardKeys.ENEMY);
            
            var combatComponent = unit.Get<IComponent_MeleeCombat>();
            if (!combatComponent.IsCombat)
            {
                combatComponent.StartCombat(new CombatOperation(target));
            }
        }

        public override void OnExit()
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