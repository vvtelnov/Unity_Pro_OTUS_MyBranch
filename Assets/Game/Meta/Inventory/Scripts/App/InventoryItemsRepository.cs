using Game.App;

namespace Game.Meta
{
    public sealed class InventoryItemsRepository : Repository
    {
        protected override string PrefsKey => "InventoryItems";

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