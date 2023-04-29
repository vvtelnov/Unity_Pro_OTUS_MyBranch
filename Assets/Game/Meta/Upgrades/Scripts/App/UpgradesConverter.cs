namespace Game.Meta
{
    public static class UpgradesConverter
    {
        public static UpgradeData[] ToInitialDataArray(UpgradeConfig[] configs)
        {
            var count = configs.Length;
            var result = new UpgradeData[count];

            for (var i = 0; i < count; i++)
            {
                var config = configs[i];
                var data = new UpgradeData
                {
                    id = config.id,
                    level = config.initialStats.level
                };

                result[i] = data;
            }

            return result;
        }
        
        public static UpgradeData[] ToDataArray(Upgrade[] upgrades)
        {
            var count = upgrades.Length;
            var result = new UpgradeData[count];

            for (var i = 0; i < count; i++)
            {
                var upgrade = upgrades[i];
                var upgradeData = new UpgradeData
                {
                    id = upgrade.Id,
                    level = upgrade.Level
                };
                result[i] = upgradeData;
            }

            return result;
        }
    }
}