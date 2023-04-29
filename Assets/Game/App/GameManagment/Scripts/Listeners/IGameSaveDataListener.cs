namespace Game.App
{
    public interface IGameSaveDataListener
    {
        void OnSaveData(GameSaveReason reason);
    }
}