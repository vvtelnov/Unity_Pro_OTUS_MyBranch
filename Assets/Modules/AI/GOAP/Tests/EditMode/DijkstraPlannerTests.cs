using System.Collections.Generic;
using NUnit.Framework;
using static AI.GOAP.TestUtils;

namespace AI.GOAP
{
    public sealed class DijkstraPlannerTests
    {
        private readonly DijkstraPlanner planner = new();

        private readonly IActor moveAtEnemyAction = new SubstituteActor(
            id: "moveAtEnemyAction",
            cost: 10,
            requiredState: new FactState(
                new Fact("enemyExists", true)
            ),
            resultState: new FactState(
                new Fact("atEnemy", true),
                new Fact("nearEnemy", true)
            )
        );

        private readonly IActor moveNearEnemyAction = new SubstituteActor(
            id: "moveNearEnemyAction",
            cost: 3,
            requiredState: new FactState(
                new Fact("enemyExists", true)
            ),
            resultState: new FactState(
                new Fact("nearEnemy", true)
            )
        );

        private readonly IActor swordCombatAction = new SubstituteActor(
            id: "swordCombatAction",
            cost: 3,
            requiredState: new FactState(
                new Fact("atEnemy", true)
            ),
            resultState: new FactState(
                new Fact("enemyExists", false)
            )
        );

        private readonly IActor bowCombatAction = new SubstituteActor(
            id: "bowCombatAction",
            cost: 6,
            requiredState: new FactState(
                new Fact("nearEnemy", true),
                new Fact("arrowsExists", true)
            ),
            resultState: new FactState(
                new Fact("enemyExists", false)
            )
        );

        private readonly IActor makeHealAction = new SubstituteActor(
            id: "makeHealAction",
            cost: 5,
            resultState: new FactState(new Fact("isInjured", false)),
            requiredState: new FactState()
        );

        private readonly IActor moveToResourceAction = new SubstituteActor(
            id: "moveToResourceAction",
            cost: 12,
            resultState: new FactState(new Fact("atResource", true)),
            requiredState: new FactState()
        );

        private readonly IActor harvestResourceAction = new SubstituteActor(
            id: "harvestResourceAction",
            cost: 5,
            resultState: new FactState(new Fact("resourceExists", true)),
            requiredState: new FactState(new Fact("atResource", true))
        );

        private readonly IActor goToMarketAction = new SubstituteActor(
            id: "goToMarketAction",
            cost: 6,
            resultState: new FactState(new Fact("moneyEnough", true)),
            requiredState: new FactState(new Fact("resourceExists", true))
        );

        private readonly IActor goBeerAction = new SubstituteActor(
            id: "goBeerAction",
            cost: 9,
            resultState: new FactState(new Fact("happy", true)),
            requiredState: new FactState(new Fact("moneyEnough", true))
        );

        [Test]
        public void MakeHealPlanTest()
        {
            //Arrange:
            var worldState = new FactState(
                new Fact("isInjured", true)
            );

            var goal = new FactState(
                new Fact("isInjured", false)
            );

            var actions = new[]
            {
                makeHealAction
            };

            //Act:
            var success = this.planner.MakePlan(worldState, goal, actions, out var actualPlan);

            //Assert:
            Assert.True(success);

            var expectedPlan = new List<IActor>
            {
                makeHealAction
            };
            Assert.True(EqualsPlans(expectedPlan, actualPlan));
        }

        [Test]
        public void MeleeCombatPlanTest()
        {
            //Arrange:
            var worldState = new FactState(
                new Fact("enemyExists", true),
                new Fact("nearEnemy", false),
                new Fact("atEnemy", false),
                new Fact("isInjured", true),
                new Fact("hasAmmo", true)
            );

            var goal = new FactState(
                new Fact("enemyExists", false)
            );

            var actions = new[]
            {
                swordCombatAction,
                moveAtEnemyAction,
                makeHealAction
            };

            //Act:
            var success = this.planner.MakePlan(worldState, goal, actions, out var actualPlan);

            //Assert:
            Assert.True(success);

            var expectedPlan = new List<IActor>
            {
                moveAtEnemyAction,
                swordCombatAction
            };
            Assert.True(EqualsPlans(expectedPlan, actualPlan));
        }

        [Test]
        public void RangeCombatPlanTest()
        {
            //Arrange:
            var worldState = new FactState(
                new Fact("enemyExists", true),
                new Fact("nearEnemy", false),
                new Fact("atEnemy", false),
                new Fact("isInjured", true),
                new Fact("arrowsExists", true)
            );

            var goal = new FactState(
                new Fact("enemyExists", false)
            );

            var actions = new []
            {
                swordCombatAction,
                bowCombatAction,
                moveNearEnemyAction,
                moveAtEnemyAction,
                moveToResourceAction,
                makeHealAction
            };

            //Act:
            var success = this.planner.MakePlan(worldState, goal, actions, out var actualPlan);

            //Assert:
            Assert.True(success);

            var expectedPlan = new List<IActor>
            {
                moveNearEnemyAction,
                bowCombatAction
            };
            Assert.True(EqualsPlans(expectedPlan, actualPlan));
        }

        [Test]
        public void DrinkBeerTest()
        {
            //Arrange:
            var worldState = new FactState(
                new Fact("enemyExists", true),
                new Fact("nearEnemy", false),
                new Fact("atEnemy", false),
                new Fact("isInjured", true),
                new Fact("hasAmmo", true),
                new Fact("moneyEnough", false),
                new Fact("resourceEnough", false),
                new Fact("atResource", false)
            );

            var goal = new FactState(
                new Fact("happy", true)
            );

            var actions = new[]
            {
                swordCombatAction,
                moveAtEnemyAction,
                makeHealAction,
                moveToResourceAction,
                harvestResourceAction,
                goToMarketAction,
                goBeerAction
            };


            //Act:
            var success = this.planner.MakePlan(worldState, goal, actions, out var actualPlan);

            //Assert:
            Assert.True(success);

            var expectedPlan = new List<IActor>
            {
                moveToResourceAction,
                harvestResourceAction,
                goToMarketAction,
                goBeerAction
            };
            Assert.True(EqualsPlans(expectedPlan, actualPlan));
        }
    }
}