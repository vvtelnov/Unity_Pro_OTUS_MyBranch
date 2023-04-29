using Game.App;
using JetBrains.Annotations;
using Services;

namespace Game.Meta
{
    [UsedImplicitly]
    public sealed class BoostersMediator : BaseMediator<BoostersRepository, BoostersManager>
    {
        [ServiceInject]
        private BoostersAssetSupplier assetSupplier;

        protected override void OnLoadData(BoostersRepository repository, BoostersManager manager)
        {
            if (repository.LoadBoosters(out var boostersData))
            {
                this.SetupBoosters(manager, boostersData);
            }
        }

        private void SetupBoosters(BoostersManager manager, BoosterData[] boostersData)
        {
            for (int i = 0, count = boostersData.Length; i < count; i++)
            {
                var data = boostersData[i];
                var config = this.assetSupplier.GetBooster(data.id);
                var booster = manager.SetupBooster(config);
                booster.RemainingTime = data.remainingTime;
            }
        }

        protected override void OnSaveData(BoostersRepository repository, BoostersManager manager)
        {
            this.SaveBoosters(repository, manager);
        }

        private void SaveBoosters(BoostersRepository repository, BoostersManager manager)
        {
            var boosters = manager.GetActiveBoosters();
            var count = boosters.Length;
            var boostersData = new BoosterData[count];

            for (var i = 0; i < count; i++)
            {
                var booster = boosters[i];
                var data = new BoosterData
                {
                    id = booster.Id,
                    remainingTime = booster.RemainingTime
                };

                boostersData[i] = data;
            }

            repository.SaveBoosters(boostersData);
        }
    }
}