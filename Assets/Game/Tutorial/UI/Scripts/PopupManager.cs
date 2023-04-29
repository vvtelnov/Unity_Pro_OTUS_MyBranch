using System;
using GameSystem;
using Windows;
using UnityEngine;

namespace Game.Tutorial.UI
{
    public sealed class PopupManager : MonoBehaviour, IWindow.Callback, IGameAttachElement
    {
        private GameContext gameContext;

        [SerializeField]
        private Transform rootTransform;

        private Action callback;

        private MonoWindow currentPopup;

        public void Show(MonoWindow prefab, object args = null, Action callback = null)
        {
            if (this.currentPopup != null)
            {
                return;
            }

            this.callback = callback;
            this.currentPopup = Instantiate(prefab, this.rootTransform);

            if (this.currentPopup.TryGetComponent(out IGameElement element))
            {
                this.gameContext.RegisterElement(element);
            }

            this.currentPopup.Show(args, callback: this);
        }

        void IWindow.Callback.OnClose(IWindow popup)
        {
            this.currentPopup.Hide();
            
            if (this.currentPopup.TryGetComponent(out IGameElement element))
            {
                this.gameContext.UnregisterElement(element);
            }
            
            Destroy(this.currentPopup.gameObject);
            this.currentPopup = null;
            this.callback?.Invoke();
        }

        void IGameAttachElement.AttachGame(GameContext context)
        {
            this.gameContext = context;
        }
    }
}