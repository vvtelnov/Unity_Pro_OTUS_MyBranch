using System;
using System.Collections;
using Game.Tutorial.UI;
using UnityEngine;

namespace Game.Tutorial.Gameplay
{
    [Serializable]
    public class InfoPanelShower
    {
        [Space]
        [SerializeField]
        private float showDelay = 1.0f;

        [SerializeField]
        private InfoPanel viewPrefab;

        protected InfoPanel view;
        
        public IEnumerator Show(Transform parent)
        {
            yield return new WaitForSeconds(this.showDelay);

            this.view = GameObject.Instantiate(this.viewPrefab, parent);
            this.OnShow();
        }

        public void Hide()
        {
            if (this.view != null)
            {
                this.OnHide();
                GameObject.Destroy(this.view.gameObject);
            }
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }
    }
}