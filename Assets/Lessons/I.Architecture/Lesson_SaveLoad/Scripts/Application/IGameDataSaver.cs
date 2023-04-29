using GameSystem;

namespace Lessons.Architecture.SaveLoad
{
    //Интерфейс для сохранения данных из игры
    public interface IGameDataSaver
    {
        void SaveData(GameContext context);
    }
}