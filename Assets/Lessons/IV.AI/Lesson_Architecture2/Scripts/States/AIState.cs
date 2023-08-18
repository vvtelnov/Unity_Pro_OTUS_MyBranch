using UnityEngine;

namespace Lessons.AI.HierarchicalStateMachine
{
    public abstract class AIState : MonoBehaviour
    {
        public virtual void OnEnter()
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnExit()
        {
        }
    }
}