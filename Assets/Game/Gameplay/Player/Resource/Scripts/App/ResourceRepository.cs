using Game.App;
using Game.GameEngine.GameResources;

namespace Game.Gameplay.Player
{
    public sealed class ResourceRepository : Repository<ResourceData[]>
    {
        protected override string PrefsKey => "PlayerBagData";
        
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