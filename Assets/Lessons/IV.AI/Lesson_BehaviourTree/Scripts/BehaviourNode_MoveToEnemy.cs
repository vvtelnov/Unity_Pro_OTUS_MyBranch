using System.Collections;
using Entities;
using Game.GameEngine.Mechanics;
using Lessons.AI.HierarchicalStateMachine;
using UnityEngine;
using Blackboard = Lessons.AI.HierarchicalStateMachine.Blackboard;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BehaviourNode_MoveToEnemy : BehaviourNode, IBehaviourCallback
    {
        [SerializeField,Space]
        private Blackboard blackboard;

        [SerializeField,Space]
        private BehaviourNode moveToPositionNode;

        private Coroutine coroutine;

        protected override void Run()
        {
            if (!this.blackboard.TryGetVariable(BlackboardKeys.ENEMY, out IEntity target))
            {
                this.Return(false);
                return;
            }

            this.coroutine = this.StartCoroutine(this.UpdateEnemyPosition(target));
            this.moveToPositionNode.Run(callback: this);
        }

        private IEnumerator UpdateEnemyPosition(IEntity target)
        {
            var enemyTransform = target.Get<IComponent_GetPosition>();
            var period = new WaitForFixedUpdate();

            while (true)
            {
                this.blackboard.SetVariable(BlackboardKeys.MOVE_POSITION, enemyTransform.Position);
                yield return period;
            }
        }

        protected override void OnAbort()
        {
            if (this.moveToPositionNode.IsRunning)
            {
                this.moveToPositionNode.Abort();
            }
        }

        protected override void OnDispose()
        {
            if (this.coroutine != null)
            {
                this.StopCoroutine(this.coroutine);
                this.coroutine = null;
            }
            
            this.blackboard.RemoveVariable(BlackboardKeys.MOVE_POSITION);
        }

        void IBehaviourCallback.Invoke(BehaviourNode node, bool success)
        {
            this.Return(success);
        }
    }
}