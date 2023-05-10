namespace Game.App
{
    public interface IRepository
    {
        void SynchronizePrefs();

        void SynchronizeClient();
    }
}