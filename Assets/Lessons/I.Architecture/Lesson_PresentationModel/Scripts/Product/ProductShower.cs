using GameSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Architecture.PresentationModel
{
    public sealed class ProductShower : MonoBehaviour, IGameConstructElement
    {
        private PopupManager popupManager;

        private ProductPresentationModelFactory presenterFactory;

        [Button]
        public void ShowProduct(Product product)
        {
            var presentationModel = this.presenterFactory.CreatePresenter(product);
            this.popupManager.ShowPopup(PopupName.PRODUCT, presentationModel);
        }

        void IGameConstructElement.ConstructGame(GameContext context)
        {
            this.popupManager = context.GetService<PopupManager>();
            this.presenterFactory = context.GetService<ProductPresentationModelFactory>();
        }
    }
}