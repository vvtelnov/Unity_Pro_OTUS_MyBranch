using GameSystem;

namespace Lessons.Architecture.SaveLoad
{
    //Интерфейс для загрузки данных в игру
    public interface IGameDataLoader
    {
        void LoadData(GameContext context);
    }
}