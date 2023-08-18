using UnityEngine;

namespace Lessons.AI.HierarchicalStateMachine
{
    public sealed class AIRootState : AIState
    {
        [SerializeField]
        private Blackboard blackboard;

        [Space]
        [SerializeField]
        private AIState patrolState;

        [SerializeField]
        private AIState attackState;

        private bool hasEnemy;

        public override void OnUpdate()
        {
            this.UpdateTransitions();

            if (this.hasEnemy)
            {
                this.attackState.OnUpdate();
            }
            else
            {
                this.patrolState.OnUpdate();
            }
        }

        public override void OnExit()
        {
            if (this.hasEnemy)
            {
                this.attackState.OnExit();
            }
            else
            {
                this.patrolState.OnExit();
            }
        }

        private void UpdateTransitions()
        {
            if (this.blackboard.HasVariable(BlackboardKeys.ENEMY) && !this.hasEnemy)
            {
                this.patrolState.OnExit();
                this.attackState.OnEnter();
                this.hasEnemy = true;
            }
            else if (!this.blackboard.HasVariable(BlackboardKeys.ENEMY) && this.hasEnemy)
            {
                this.attackState.OnExit();
                this.patrolState.OnEnter();
                this.hasEnemy = false;
            }
        }
    }
}