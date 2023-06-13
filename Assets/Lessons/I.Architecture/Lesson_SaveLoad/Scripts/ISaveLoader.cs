using GameSystem;

namespace Lessons.Architecture.SaveLoad
{
    public interface ISaveLoader
    {
        void LoadGame(GameContext context);

        void SaveGame(GameContext context);
    }
}