using Game.App;

namespace Game.Gameplay.Player
{
    public class MoneyRepository : DataRepository<int>
    {
        protected override string Key => "MoneyData";

        public virtual bool LoadMoney(out int money)
        {
            return this.LoadData(out money);
        }

        public virtual void SaveMoney(int money)
        {
            this.SaveData(money);
        }
    }
}