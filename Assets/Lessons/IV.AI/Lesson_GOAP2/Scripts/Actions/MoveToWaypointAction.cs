using System.Collections;
using AI.GOAP;
using Entities;
using Game.GameEngine.Mechanics;
using Lessons.AI.Architecture;
using Lessons.AI.HierarchicalStateMachine;
using UnityEngine;

namespace Lessons.AI.Lesson_GOAP2
{
    public sealed class MoveToWaypointAction : Actor
    {
        [SerializeField]
        private Blackboard blackboard;

        [SerializeField]
        private MoveToPositionAgent moveAgent;

        private Coroutine coroutine;

        public override bool IsValid()
        {
            return this.blackboard.HasVariable(BlackboardKeys.UNIT) &&
                   this.blackboard.HasVariable(BlackboardKeys.WAYPOINTS) &&
                   this.blackboard.HasVariable(BlackboardKeys.WAYPOINT_INDEX) &&
                   this.blackboard.HasVariable(BlackboardKeys.STOPPING_DISTANCE);
        }

        public override int EvaluateCost()
        {
            var unit = this.blackboard.GetVariable<IEntity>(BlackboardKeys.UNIT);
            var waypoints = this.blackboard.GetVariable<Transform[]>(BlackboardKeys.WAYPOINTS);
            var waypointIndex = this.blackboard.GetVariable<int>(BlackboardKeys.WAYPOINTS);

            var unitPosition = unit.Get<IComponent_GetPosition>().Position;
            var waypointPosition = waypoints[waypointIndex].position;

            var distance = Vector3.Distance(unitPosition, waypointPosition);
            return Mathf.RoundToInt(distance);
        }

        protected override void Play()
        {
            var unit = this.blackboard.GetVariable<IEntity>(BlackboardKeys.UNIT);
            var stoppingDistance = this.blackboard.GetVariable<float>(BlackboardKeys.STOPPING_DISTANCE);
            var waypoints = this.blackboard.GetVariable<Transform[]>(BlackboardKeys.WAYPOINTS);
            var waypointIndex = this.blackboard.GetVariable<int>(BlackboardKeys.WAYPOINTS);

            this.moveAgent.SetUnit(unit);
            this.moveAgent.SetStoppingDistance(stoppingDistance);
            this.moveAgent.SetTargetPosiiton(waypoints[waypointIndex]);
            this.moveAgent.Play();

            this.coroutine = this.StartCoroutine(this.CheckReachedRoutine());
        }

        private IEnumerator CheckReachedRoutine()
        {
            while (!this.moveAgent.IsReached)
            {
                yield return new WaitForFixedUpdate();
            }

            this.Return(true);
        }

        protected override void OnDispose()
        {
            if (this.coroutine != null)
            {
                this.StopCoroutine(this.coroutine);
                this.coroutine = null;
            }
        }
    }
}