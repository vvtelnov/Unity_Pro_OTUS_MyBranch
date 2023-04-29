using System.Collections.Generic;
using GameSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.GameEngine
{
    public class PopupModule : GameModule
    {
        [SerializeField]
        private PopupCatalog catalog;

        [SerializeField]
        private Transform container;
        
        [ShowInInspector]
        private readonly PopupManager manager = new();

        private readonly PopupSupplier supplier = new();

        private readonly PopupFactory factory = new();

        public override IEnumerable<IGameElement> GetElements()
        {
            yield break;
        }

        public override IEnumerable<object> GetServices()
        {
            yield return this.manager;
        }

        public override void ConstructGame(GameContext context)
        {
            this.factory.Construct(this.catalog, this.container);
            this.supplier.Construct(context, this.factory);
            this.manager.SetSupplier(this.supplier);
        }
    }
}