using Game.App;

namespace Game.Meta
{
    public sealed class MissionsRepository : Repository
    {
        protected override string PrefsKey => "MissionsData";

        public bool LoadMissions(out MissionData[] missions)
        {
            return this.LoadData(out missions);
        }

        public void SaveMissions(MissionData[] missions)
        {
            this.SaveData(missions);
        }

        public void DeleteMissions()
        {
            this.ClearData();
        }
    }
}