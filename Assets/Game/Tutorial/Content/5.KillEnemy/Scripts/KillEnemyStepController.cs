using Entities;
using Game.GameEngine;
using Game.Tutorial.Gameplay;
using Game.Tutorial.UI;
using GameSystem;
using UnityEngine;

namespace Game.Tutorial
{
    [AddComponentMenu("Tutorial/Step «Kill Enemy»")]
    public sealed class KillEnemyStepController : TutorialStepController
    {
        private readonly KillEnemyInspector actionInspector = new();

        private ScreenTransform screenTransform;

        [SerializeField]
        private KillEnemyConfig config;

        [SerializeField]
        private KillEnemyManager enemyManager;

        [SerializeField]
        private KillEnemyPanelShower panelShower;

        public override void ConstructGame(GameContext context)
        {
            this.screenTransform = context.GetService<ScreenTransform>();

            this.enemyManager.Construct(context);
            this.panelShower.Construct(this.config);
            base.ConstructGame(context);
        }

        protected override void OnStart()
        {
            var enemy = this.enemyManager.SpawnEnemy();
            this.actionInspector.Inspect(enemy, this.OnEnemyDestroyed);
            this.StartCoroutine(this.panelShower.Show(this.screenTransform.Value));
        }

        private void OnEnemyDestroyed(IEntity enemy)
        {
            this.panelShower.Hide();
            this.StartCoroutine(this.enemyManager.DestroyEnemy(enemy as MonoEntity));
            this.NotifyAboutCompleteAndMoveNext();
        }
    }
}