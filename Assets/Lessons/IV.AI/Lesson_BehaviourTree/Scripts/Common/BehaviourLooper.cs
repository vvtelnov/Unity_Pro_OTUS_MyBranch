using UnityEngine;

namespace Lessons.AI.LessonBehaviourTree
{
    public sealed class BehaviourLooper : MonoBehaviour
    {
        [SerializeField]
        private BehaviourNode node;
        
        private void FixedUpdate()
        {
            if (!this.node.IsRunning)
            {
                this.node.Run(null);
            }
        }
    }
}