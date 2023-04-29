using Game.App;
using Services;

namespace Game.Meta
{
    public sealed class MisssionsMediator : BaseMediator<MissionsDao, MissionsManager>
    {
        [ServiceInject]
        private MissionsAssetSupplier assetSupplier;

        protected override void OnLoadData(MissionsDao dao, MissionsManager manager)
        {
            if (!dao.SelectMissions(out var missionsData))
            {
                return;
            }

            for (int i = 0, count = missionsData.Count; i < count; i++)
            {
                var data = missionsData[i];
                var config = this.assetSupplier.GetMission(data.id);
                var mission = manager.SetupMission(config);
                config.DeserializeTo(data.serializedState, mission);
            }
        }

        protected override void OnSaveData(MissionsDao dao, MissionsManager manager)
        {
            dao.DeleteMissions();
            
            var actualMissions = manager.GetMissions();
            var count = actualMissions.Length;
            var dataArray = new MissionData[count];

            for (var i = 0; i < count; i++)
            {
                var mission = actualMissions[i];
                var data = this.ConvertToData(mission);
                dataArray[i] = data;
            }

            dao.InsertMissions(dataArray);
        }
        
        private MissionData ConvertToData(Mission mission)
        {
            var id = mission.Id;
            var config = this.assetSupplier.GetMission(id);
            var data = new MissionData
            {
                id = id,
                serializedState = config.Serialize(mission)
            };

            return data;
        }
    }
}