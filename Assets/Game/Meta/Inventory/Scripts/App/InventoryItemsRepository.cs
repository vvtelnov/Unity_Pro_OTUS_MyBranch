using Game.App;

namespace Game.Meta
{
    public sealed class InventoryItemsRepository : DataArrayRepository<InventoryItemData>
    {
        protected override string Key => "InventoryItems";

        public bool LoadItems(out InventoryItemData[] items)
        {
            return this.LoadData(out items);
        }

        public void SaveItems(InventoryItemData[] items)
        {
            this.SaveData(items);
        }
    }
}