using Lessons.AI.HierarchicalStateMachine;
using UnityEngine;
using Blackboard = Lessons.AI.HierarchicalStateMachine.Blackboard;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BehaviourAborter_EnemyChanged : MonoBehaviour
    {
        [SerializeField]
        private BehaviourNode node;

        [Space, SerializeField]
        private Blackboard blackboard;

        private void OnEnable()
        {
            this.blackboard.OnVariableRemoved += this.OnVariableChanged;
            this.blackboard.OnVariableChanged += this.OnVariableChanged;
        }
        
        private void OnDisable()
        {
            this.blackboard.OnVariableRemoved -= this.OnVariableChanged;
            this.blackboard.OnVariableChanged -= this.OnVariableChanged;
        }

        private void OnVariableChanged(string key, object _)
        {
            if (key == BlackboardKeys.ENEMY)
            {
                this.node.Abort();
            }
        }
    }
}