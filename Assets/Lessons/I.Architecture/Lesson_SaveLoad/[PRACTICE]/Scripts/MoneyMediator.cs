using GameSystem;
using Services;

namespace Lessons.Architecture.SaveLoad
{
    public sealed class MoneyMediator : IGameDataLoader, IGameDataSaver
    {
        [ServiceInject]
        private MoneyRepository repository;

        void IGameDataLoader.LoadData(GameContext context)
        {
            if (this.repository.LoadMoney(out var money))
            {
                var moneyStorage = context.GetService<MoneyStorage>();
                moneyStorage.SetupMoney(money);
            }
        }

        void IGameDataSaver.SaveData(GameContext context)
        {
            var moneyStorage = context.GetService<MoneyStorage>();
            this.repository.SaveMoney(moneyStorage.Money);
        }
    }
}