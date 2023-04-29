using System;
using GameSystem;
using Lessons.Architecture.MVA;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Architecture.PresentationModel
{
    public sealed class ProductBuyer : MonoBehaviour, IGameConstructElement
    {
        private MoneyStorage moneyStorage;

        [Button]
        public bool CanBuy(Product product)
        {
            return this.moneyStorage.Money >= product.price;
        }

        [Button]
        public void Buy(Product product)
        {
            if (this.CanBuy(product))
            {
                this.moneyStorage.SpendMoney(product.price);
                Debug.Log($"<color=green>Product {product.title} successfully purchased!</color>");
            }
            else
            {
                Debug.LogWarning($"<color=red>Not enough money for product {product.title}!</color>");
            }
        }

        void IGameConstructElement.ConstructGame(GameContext context)
        {
            this.moneyStorage = context.GetService<MoneyStorage>();
        }
    }
}