using Game.App;

namespace Game.Meta
{
    public sealed class BoostersRepository : DataArrayRepository<BoosterData>
    {
        protected override string Key => "Boosters";

        public bool LoadBoosters(out BoosterData[] boostersData)
        {
            return this.LoadData(out boostersData);
        }

        public void SaveBoosters(BoosterData[] boostersData)
        {
            this.SaveData(boostersData);
        }
    }
}