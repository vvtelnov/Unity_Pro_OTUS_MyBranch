using AI.GOAP;
using Entities;
using Game.GameEngine.Entities;
using Game.GameEngine.Mechanics;
using Lessons.AI.HierarchicalStateMachine;
using UnityEngine;

namespace Lessons.AI.Lesson_GOAP2
{
    public sealed class EnemyFactInspector : FactInspector
    {
        [SerializeField]
        private Blackboard blackboard;

        [Space]
        [SerializeField, FactId]
        private string hasEnemy;

        [SerializeField, FactId]
        private string nearEnemy;

        [SerializeField, FactId]
        private string atEnemy;

        public override void OnUpdate(WorldState worldState)
        {
            if (this.blackboard.TryGetVariable(BlackboardKeys.UNIT, out IEntity unit) &&
                this.blackboard.TryGetVariable(BlackboardKeys.ENEMY, out IEntity enemy))
            {
                var currentDistance = EntityUtils.Distance(unit, enemy);
                var nearDistance = this.blackboard.GetVariable<float>(BlackboardKeys.NEAR_ENEMY_DISTANCE);
                var atDistance = this.blackboard.GetVariable<float>(BlackboardKeys.AT_ENEMY_DISTANCE);
                
                worldState.SetFact(this.hasEnemy, true);
                worldState.SetFact(this.nearEnemy, currentDistance <= nearDistance);
                worldState.SetFact(this.atEnemy, currentDistance <= atDistance);
            }
            else
            {
                worldState.RemoveFact(this.hasEnemy);
                worldState.RemoveFact(this.nearEnemy);
                worldState.RemoveFact(this.atEnemy);
            }
        }
    }
}