using Game.App;

namespace Game.Meta
{
    public sealed class UpgradesRepository : DataArrayRepository<UpgradeData>
    {
        protected override string Key => "HeroUpgradesData";

        public bool LoadUpgrades(out UpgradeData[] upgrades)
        {
            return this.LoadData(out upgrades);
        }

        public void SaveUpgrades(UpgradeData[] upgrades)
        {
            this.SaveData(upgrades);
        }
    }
}