using Game.App;
using Services;

namespace Game.Meta
{
    public sealed class UpgradesMediator : LazyMediator<UpgradesRepository, UpgradesManager>
    {
        [ServiceInject]
        private UpgradesAssetSupplier assetSupplier;

        protected override void OnLoadData(UpgradesRepository repository, UpgradesManager manager)
        {
            if (!repository.LoadUpgrades(out var upgradesData))
            {
                var configs = this.assetSupplier.GetAllUpgrades();
                upgradesData = UpgradesConverter.ToInitialDataArray(configs);
            }

            for (int i = 0, count = upgradesData.Length; i < count; i++)
            {
                var data = upgradesData[i];
                var upgrade = manager.GetUpgrade(data.id);
                upgrade.SetupLevel(data.level);
            }
        }

        protected override void OnSaveData(UpgradesRepository repository, UpgradesManager manager)
        {
            var upgrades = manager.GetAllUpgrades();
            var dataSet = UpgradesConverter.ToDataArray(upgrades);
            repository.SaveUpgrades(dataSet);
        }

        protected override void OnStartGame(UpgradesManager manager)
        {
            manager.OnLevelUp += this.OnUpgradeLevelUp;
        }

        protected override void OnStopGame(UpgradesManager manager)
        {
            manager.OnLevelUp -= this.OnUpgradeLevelUp;
        }

        private void OnUpgradeLevelUp(Upgrade _)
        {
            this.MarkSaveRequired();
        }
    }
}