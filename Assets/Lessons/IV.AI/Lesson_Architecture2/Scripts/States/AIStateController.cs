using UnityEngine;

namespace Lessons.AI.HierarchicalStateMachine
{
    public sealed class AIStateController : MonoBehaviour
    {
        [SerializeField]
        private AIState state;
        
        private void OnEnable()
        {
            this.state.OnEnter();
        }

        private void FixedUpdate()
        {
            this.state.OnUpdate();
        }

        private void OnDisable()
        {
            this.state.OnExit();
        }
    }
}