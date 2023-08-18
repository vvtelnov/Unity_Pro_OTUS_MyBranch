using System;
using System.Linq;
using AI.Iterators;
using Entities;
using UnityEngine;
using static Lessons.AI.HierarchicalStateMachine.BlackboardKeys;

namespace Lessons.AI.HierarchicalStateMachine
{
    public sealed class BlackboardInstaller : MonoBehaviour
    {
        public MonoEntity unit;
        public Transform[] waypoints;

        private void Awake()
        {
            var blackboard = this.GetComponent<Blackboard>();
            blackboard.SetVariable(UNIT, this.unit);
            blackboard.SetVariable(STOPPING_DISTANCE, 0.25f);
            blackboard.SetVariable(PATROL_PAUSE, 1.0f);
            blackboard.SetVariable(WAYPOINTS, IteratorFactory.CreateIterator<Vector3>(
                IteratorMode.CIRCLE,
                this.waypoints.Select(it => it.position).ToArray()
            ));
        }
    }
}