using System.Collections;
using Lessons.AI.HierarchicalStateMachine;
using UnityEngine;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BehaviourNode_PatrolPause : BehaviourNode
    {
        [SerializeField]
        private Blackboard blackboard;

        private Coroutine coroutine;

        protected override void Run()
        {
            if (!this.blackboard.TryGetVariable(BlackboardKeys.PATROL_IDLE_TIME, out float pauseTime))
            {
                this.Return(false);
                return;
            }

            this.coroutine = this.StartCoroutine(this.PatrolPause(pauseTime));
        }

        protected override void OnDispose()
        {
            if (this.coroutine != null)
            {
                this.StopCoroutine(this.coroutine);
                this.coroutine = null;
            }
        }
        
        private IEnumerator PatrolPause(float pauseTime)
        {
            yield return new WaitForSeconds(pauseTime);
            this.coroutine = null;
            this.Return(true);
        }
    }
}