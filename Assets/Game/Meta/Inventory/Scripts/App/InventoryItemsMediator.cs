using Game.App;
using Game.GameEngine.InventorySystem;
using Services;

namespace Game.Meta
{
    public sealed class InventoryItemsMediator :
        IGameLoadDataListener,
        IGameSaveDataListener
    {
        [ServiceInject]
        private InventoryItemsRepository repository;

        [ServiceInject]
        private InventoryItemsAssetSupplier assetSupplier;

        private StackableInventory playerInventory;

        void IGameLoadDataListener.OnLoadData(GameContainer gameContainer)
        {
            this.playerInventory = gameContainer.GetService<InventoryService>().GetInventory();
            if (this.repository.LoadItems(out var itemsData))
            {
                this.SetupItems(itemsData);
            }
        }

        void IGameSaveDataListener.OnSaveData(GameSaveReason reason)
        {
            this.SaveItems();
        }

        private void SetupItems(InventoryItemData[] itemsData)
        {
            for (int i = 0, count = itemsData.Length; i < count; i++)
            {
                var data = itemsData[i];
                var config = this.assetSupplier.GetItem(data.name);
                this.playerInventory.AddItemsByPrototype(config.Prototype, data.count);
            }
        }

        private void SaveItems()
        {
            var inventoryItems = this.playerInventory.CountAllItemsInDictionary();
            var count = inventoryItems.Count;
            var itemsData = new InventoryItemData[count];
            var index = 0;

            foreach (var (name, amount) in inventoryItems)
            {
                var data = new InventoryItemData
                {
                    name = name,
                    count = amount
                };

                itemsData[index] = data;
                index++;
            }

            this.repository.SaveItems(itemsData);
        }
    }
}