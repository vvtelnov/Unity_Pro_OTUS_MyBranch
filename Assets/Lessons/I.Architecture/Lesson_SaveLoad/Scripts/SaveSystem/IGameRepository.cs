namespace Lessons.Architecture.SaveLoad
{
    public interface IGameRepository
    {
        string GetData(string key);

        bool TryGetData(string key, out string value);

        void SetData(string key, string value);
    }
}