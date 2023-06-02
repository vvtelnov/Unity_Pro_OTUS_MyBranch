using GameSystem;
using Lessons.Architecture.MVO;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PM
{
    public sealed class ProductShower : MonoBehaviour
    {
        private PopupManager popupManager;
        
        private ProductBuyer productBuyer;

        private MoneyStorage moneyStorage;

        [GameInject]
        public void Construct(PopupManager popupManager, ProductBuyer productBuyer, MoneyStorage moneyStorage)
        {
            this.popupManager = popupManager;
            this.productBuyer = productBuyer;
            this.moneyStorage = moneyStorage;
        }

        [Button]
        public void ShowProduct(Product product)
        {
            //Нарушаю GRASP CREATOR
            var presentationModel = new ProductPresentationModel(product, this.productBuyer, this.moneyStorage);
            this.popupManager.ShowPopup(PopupName.PRODUCT, presentationModel);
        }
    }
}