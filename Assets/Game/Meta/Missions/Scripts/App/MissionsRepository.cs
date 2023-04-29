using Game.App;

namespace Game.Meta
{
    public sealed class MissionsRepository : DataArrayRepository<MissionData>
    {
        protected override string Key => "MissionsData";

        public bool LoadMissions(out MissionData[] missions)
        {
            return this.LoadData(out missions);
        }

        public void SaveMissions(MissionData[] missions)
        {
            this.SaveData(missions);
        }
    }
}