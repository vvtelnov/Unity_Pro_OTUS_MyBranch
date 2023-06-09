using Game.GameEngine.GameResources;
using Game.Gameplay.Player;
using GameSystem;

namespace Lessons.Architecture.Loading
{
    public sealed class DataLoader
    {
        public void LoadData(GameContext context)
        {
            context.GetService<MoneyStorage>().SetupMoney(100);
            
            context.GetService<ResourceStorage>().Setup(new[]
            {
                new ResourceData(ResourceType.WOOD, 10),
                new ResourceData(ResourceType.STONE, 20),
                new ResourceData(ResourceType.LUMBER, 50)
            });
        }
    }
}