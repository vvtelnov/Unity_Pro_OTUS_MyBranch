using Game.App;

namespace Game.Gameplay.Conveyors
{
    public sealed class ConveyorsRepository : Repository
    {
        protected override string PrefsKey => "ConveyorsData";

        public bool LoadConveyors(out ConveyorData[] conveyors)
        {
            return this.LoadData(out conveyors);
        }

        public void SaveConveyors(ConveyorData[] conveyors)
        {
            this.SaveData(conveyors);
        }
    }
}