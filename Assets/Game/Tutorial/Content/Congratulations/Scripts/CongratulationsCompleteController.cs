using System.Collections;
using Game.Localization;
using Game.Tutorial.Gameplay;
using Game.Tutorial.UI;
using GameSystem;
using Windows;
using UnityEngine;

namespace Game.Tutorial
{
    [AddComponentMenu("Tutorial/Complete «Congratulations»")]
    public sealed class CongratulationsCompleteController : TutorialCompleteController
    {
        [SerializeField]
        private MonoWindow popupPrefab;

        [SerializeField]
        private CongratulationsConfig config;

        [SerializeField]
        private float showPopupDelay = 0.5f;
        
        private PopupManager popupManager;

        public override void ConstructGame(GameContext context)
        {
            base.ConstructGame(context);
            this.popupManager = context.GetService<PopupManager>();
        }

        protected override void OnTutorialComplete()
        {
            this.StartCoroutine(this.ShowPopupRoutine());
        }

        private IEnumerator ShowPopupRoutine()
        {
            yield return new WaitForSeconds(this.showPopupDelay);
            
            var title = LocalizationManager.GetCurrentText(this.config.title);
            var description = LocalizationManager.GetCurrentText(this.config.description);
            
            var args = new CongratulationsArgs(title, description);
            this.popupManager.Show(this.popupPrefab, args);
        }
    }
}