namespace Game.App
{
    public abstract class DataArrayRepository<T>
    {
        protected abstract string Key { get; }

        protected bool LoadData(out T[] dataArray)
        {
            if (PlayerPreferences.KeyExists(this.Key))
            {
                dataArray = PlayerPreferences.Load<T[]>(this.Key);
                return true;
            }

            dataArray = null;
            return false;
        }

        protected void SaveData(T[] data)
        {
            PlayerPreferences.Save(this.Key, data);
        }
    }
}