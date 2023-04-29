using Game.App;
using JetBrains.Annotations;
using Services;

namespace Game.Gameplay.Player
{
    [UsedImplicitly]
    public sealed class MoneyMediator : LazyMediator<MoneyRepository, MoneyStorage>
    {
        protected override void OnLoadData(MoneyRepository repository, MoneyStorage storage)
        {
            if (!repository.LoadMoney(out var money))
            {
                var config = MoneyStorageConfig.LoadAsset();
                money = config.InitialMoney;
            }

            storage.SetupMoney(money);
        }

        protected override void OnSaveData(MoneyRepository repository, MoneyStorage storage)
        {
            repository.SaveMoney(storage.Money);
        }

        protected override void OnStartGame(MoneyStorage moneyStorage)
        {
            moneyStorage.OnMoneyChanged += this.OnMoneyChanged;
        }

        protected override void OnStopGame(MoneyStorage moneyStorage)
        {
            moneyStorage.OnMoneyChanged -= this.OnMoneyChanged;
        }

        private void OnMoneyChanged(int money)
        {
            this.MarkSaveRequired();
        }
    }
}