using AI.Blackboards;
using Entities;
using UnityEngine;
using static Lessons.AI.HierarchicalStateMachine.BlackboardKeys;

namespace Lessons.AI.HierarchicalStateMachine
{
    [RequireComponent(typeof(Blackboard))]
    public sealed class BlackboardInstaller : MonoBehaviour
    {
        [SerializeField]
        private MonoEntity unit;

        [SerializeField]
        private Transform movePoint;
        
        [SerializeField]
        private Transform[] waypoints;

        [SerializeField, BlackboardKey]
        private string unitKey;

        private void Awake()
        {
            var blackboard = this.GetComponent<Blackboard>();
            blackboard.SetVariable(UNIT, this.unit);
            blackboard.SetVariable(MOVE_POSITION, this.movePoint.position);
            blackboard.SetVariable(STOPPING_DISTANCE, 0.25f);
            blackboard.SetVariable(WAYPOINTS, this.waypoints);
            blackboard.SetVariable(WAYPOINT_INDEX, 0);
            blackboard.SetVariable(PATROL_IDLE_TIME, 1.0f);
        }
    }
}