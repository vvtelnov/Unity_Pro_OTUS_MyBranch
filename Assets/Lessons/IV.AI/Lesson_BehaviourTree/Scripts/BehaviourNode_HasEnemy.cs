using Lessons.AI.HierarchicalStateMachine;
using UnityEngine;
using Blackboard = Lessons.AI.HierarchicalStateMachine.Blackboard;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BehaviourNode_HasEnemy : BehaviourNode
    {
        [SerializeField]
        private Blackboard blackboard;

        protected override void Run()
        {
            var hasEnemy = this.blackboard.HasVariable(BlackboardKeys.ENEMY);
            this.Return(hasEnemy);
        }
    }
}