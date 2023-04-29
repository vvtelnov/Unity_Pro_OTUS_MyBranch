using Entities;
using Game.App;
using Game.GameEngine.Mechanics;

namespace Game.Gameplay.Conveyors
{
    public sealed class ConveyorsMediator : BaseMediator<ConveyorsRepository, ConveyorsService>
    {
        protected override void OnLoadData(ConveyorsRepository repository, ConveyorsService service)
        {
            if (!repository.LoadConveyors(out var conveyorsData))
            {
                return;
            }

            for (int i = 0, count = conveyorsData.Length; i < count; i++)
            {
                var data = conveyorsData[i];
                var conveyor = service.FindConveyor(data.id);
                SetupConveyor(conveyor, data);
            }
        }

        protected override void OnSaveData(ConveyorsRepository repository, ConveyorsService service)
        {
            var conveyors = service.GetAllConveyors();
            var count = conveyors.Length;
            var dataArray = new ConveyorData[count];

            for (var i = 0; i < count; i++)
            {
                var conveyor = conveyors[i];
                var data = ConvertToData(conveyor);
                dataArray[i] = data;
            }

            repository.SaveConveyors(dataArray);
        }

        private static void SetupConveyor(IEntity conveyor, ConveyorData data)
        {
            conveyor
                .Get<IComponent_LoadZone>()
                .SetupAmount(data.inputAmount);
            conveyor
                .Get<IComponent_UnloadZone>()
                .SetupAmount(data.outputAmount);
        }

        private static ConveyorData ConvertToData(IEntity conveyor)
        {
            var data = new ConveyorData
            {
                id = conveyor.Get<IComponent_GetId>().Id,
                inputAmount = conveyor.Get<IComponent_LoadZone>().CurrentAmount,
                outputAmount = conveyor.Get<IComponent_UnloadZone>().CurrentAmount
            };
            return data;
        }
    }
}