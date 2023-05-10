using Game.App;
using Services;

namespace Game.Meta
{
    public sealed class MisssionsMediator : BaseMediator<MissionsRepository, MissionsManager>
    {
        [ServiceInject]
        private MissionsAssetSupplier assetSupplier;

        protected override void OnLoadData(MissionsRepository repository, MissionsManager manager)
        {
            if (!repository.LoadMissions(out var missionsData))
            {
                return;
            }

            for (int i = 0, count = missionsData.Length; i < count; i++)
            {
                var data = missionsData[i];
                var config = this.assetSupplier.GetMission(data.id);
                var mission = manager.SetupMission(config);
                config.DeserializeTo(data.serializedState, mission);
            }
        }

        protected override void OnSaveData(MissionsRepository repository, MissionsManager manager)
        {
            repository.DeleteMissions();
            
            var actualMissions = manager.GetMissions();
            var count = actualMissions.Length;
            var dataArray = new MissionData[count];

            for (var i = 0; i < count; i++)
            {
                var mission = actualMissions[i];
                var data = this.ConvertToData(mission);
                dataArray[i] = data;
            }

            repository.SaveMissions(dataArray);
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