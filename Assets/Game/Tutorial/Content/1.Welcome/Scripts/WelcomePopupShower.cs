using System;
using System.Collections;
using Game.App;
using Game.Localization;
using Game.Tutorial.UI;
using Windows;
using UnityEngine;

namespace Game.Tutorial
{
    [Serializable]
    public sealed class WelcomePopupShower 
    {
        [SerializeField]
        private MonoWindow popupPrefab;
        
        [SerializeField]
        private float showPopupDelay = 0.5f;

        private PopupManager popupManager;

        private WelcomeConfig config;

        public void Construct(PopupManager popupManager, WelcomeConfig config)
        {
            this.popupManager = popupManager;
            this.config = config;
        }
    
        public IEnumerator ShowPopup(Action callback)
        {
            yield return new WaitForSeconds(this.showPopupDelay);
            
            var language = LanguageManager.CurrentLanguage;
            var title = LocalizationManager.GetText(this.config.title, language);
            var description = LocalizationManager.GetText(this.config.description, language);
            
            var args = new WelcomeArgs(title, description);
            this.popupManager.Show(this.popupPrefab, args, callback);
        }
    }
}