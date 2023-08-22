using System.Collections;
using Entities;
using Game.GameEngine.Mechanics;
using Lessons.AI.HierarchicalStateMachine;
using UnityEngine;
using Blackboard = Lessons.AI.HierarchicalStateMachine.Blackboard;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BehaviourNode_MoveToPosition : BehaviourNode
    {
        [SerializeField]
        private Blackboard blackboard;

        private Coroutine coroutine;

        protected override void Run()
        {
            if (!this.blackboard.TryGetVariable(BlackboardKeys.UNIT, out IEntity unit))
            {
                this.Return(false);
                return;
            }

            this.coroutine = this.StartCoroutine(this.MoveToPosition(unit));
        }

        protected override void OnDispose()
        {
            if (this.coroutine != null)
            {
                this.StopCoroutine(this.coroutine);
                this.coroutine = null;
            }
        }

        private IEnumerator MoveToPosition(IEntity unit)
        {
            var posiitonComponent = unit.Get<IComponent_GetPosition>();
            var moveComponent = unit.Get<IComponent_MoveInDirection>();
            var period = new WaitForFixedUpdate();

            while (true)
            {
                var stoppingDistance = this.blackboard.GetVariable<float>(BlackboardKeys.STOPPING_DISTANCE);
                var targetPosition = this.blackboard.GetVariable<Vector3>(BlackboardKeys.MOVE_POSITION);
                
                var distanceVector = targetPosition - posiitonComponent.Position;
                var distance = distanceVector.magnitude;
                if (distance <= stoppingDistance)
                {
                    break;
                }

                var direction = distanceVector.normalized;
                moveComponent.Move(direction);

                yield return period;
            }

            yield return period; //Костыль...
            
            this.coroutine = null;
            this.Return(true);
        }
    }
}