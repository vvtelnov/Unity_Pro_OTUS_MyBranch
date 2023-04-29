using System;
using Game.Tutorial.Gameplay;
using Game.Tutorial.UI;
using UnityEngine;

namespace Lessons.Plugins.Lesson_Tutorial2
{
    [Serializable]
    public sealed class UpgradeHeroPanelShower : InfoPanelShower
    {
        private UpgradeHeroStepConfig config;

        private ScreenTransform screenTransform;

        private MonoBehaviour coroutineDispatcher;

        public void Construct(
            UpgradeHeroStepConfig config,
            ScreenTransform screenTransform,
            MonoBehaviour coroutineDispatcher
        )
        {
            this.config = config;
            this.screenTransform = screenTransform;
            this.coroutineDispatcher = coroutineDispatcher;
        }

        public void Show()
        {
            var parent = this.screenTransform.Value;
            this.coroutineDispatcher.StartCoroutine(this.Show(parent));
        }

        protected override void OnShow()
        {
            this.view.SetIcon(this.config.icon);
            this.view.SetTitle(this.config.title);
        }
    }
}