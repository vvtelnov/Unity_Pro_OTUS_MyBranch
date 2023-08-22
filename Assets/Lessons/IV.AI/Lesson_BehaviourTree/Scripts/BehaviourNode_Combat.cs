using Entities;
using Game.GameEngine.Mechanics;
using Lessons.AI.HierarchicalStateMachine;
using UnityEngine;
using Blackboard = Lessons.AI.HierarchicalStateMachine.Blackboard;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BehaviourNode_Combat : BehaviourNode
    {
        [SerializeField]
        private Blackboard blackboard;

        private IComponent_MeleeCombat unitComponent;

        protected override void Run()
        {
            if (!this.blackboard.TryGetVariable(BlackboardKeys.UNIT, out IEntity unit))
            {
                this.Return(false);
                return;
            }

            if (!this.blackboard.TryGetVariable(BlackboardKeys.ENEMY, out IEntity target))
            {
                this.Return(false);
                return;
            }

            this.unitComponent = unit.Get<IComponent_MeleeCombat>();
            this.StartCombat(target);
        }

        private void StartCombat(IEntity target)
        {
            if (this.unitComponent.IsCombat)
            {
                this.unitComponent.StopCombat();
            }

            this.unitComponent.OnCombatStopped += this.OnCombatFinished;
            
            var operation = new CombatOperation(target);
            
            if (this.unitComponent.CanStartCombat(operation))
            {
                this.unitComponent.StartCombat(operation);
            }
            else
            {
                this.Return(true);
            }
        }

        private void OnCombatFinished(CombatOperation obj)
        {
            this.unitComponent.OnCombatStopped -= this.OnCombatFinished;
            this.Return(true);
        }

        protected override void OnAbort()
        {
            this.unitComponent.OnCombatStopped -= this.OnCombatFinished;
            if (this.unitComponent.IsCombat)
            {
                this.unitComponent.StopCombat();
            }
        }
    }
}