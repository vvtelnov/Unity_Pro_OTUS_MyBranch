using Game.App;
using Game.GameEngine.GameResources;

namespace Game.Gameplay.Player
{
    public sealed class ResourceRepository : DataArrayRepository<ResourceData>
    {
        private const string PLAYER_BAG_DATA = "PlayerBagData";

        protected override string Key => PLAYER_BAG_DATA;
        
        public bool LoadResources(out ResourceData[] gameResources)
        {
            return this.LoadData(out gameResources);
        }

        public void SaveResources(ResourceData[] gameResources)
        {
            this.SaveData(gameResources);
        }
    }
}