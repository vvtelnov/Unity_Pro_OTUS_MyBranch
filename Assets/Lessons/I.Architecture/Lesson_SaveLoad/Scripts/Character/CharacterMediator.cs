using Entities;
using Game.GameEngine.Mechanics;
using GameSystem;
using Services;

namespace Lessons.Architecture.SaveLoad
{
    public sealed class CharacterMediator : IGameDataLoader, IGameDataSaver
    {
        private CharacterRepository repository;

        [ServiceInject]
        public void Construct(CharacterRepository repository)
        {
            this.repository = repository;
        }

        void IGameDataLoader.LoadData(GameContext context)
        {
            if (this.repository.LoadStats(out CharacterData data))
            {
                IEntity character = context.GetService<CharacterService>().GetCharacter();
                CharacterConverter.SetupStats(character, data);
            }
        }

        void IGameDataSaver.SaveData(GameContext context)
        {
            var character = context.GetService<CharacterService>().GetCharacter();
            var data = CharacterConverter.ConvertToData(character);
            this.repository.SaveStats(data);
        }
    }
}