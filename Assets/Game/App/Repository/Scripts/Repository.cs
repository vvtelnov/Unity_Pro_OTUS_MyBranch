using Services;

namespace Game.App
{
    public abstract class Repository<T> : IRepository
    {
        protected abstract string PrefsKey { get; }

        [ServiceInject]
        private PlayerClient playerClient;

        void IRepository.SynchronizePrefs()
        {
            var key = this.PrefsKey;
            if (this.playerClient.TryGetData<T>(key, out var data))
            {
                PlayerPreferences.Save(key, data);
            }
        }

        void IRepository.SynchronizeClient()
        {
            var key = this.PrefsKey;
            if (PlayerPreferences.TryLoad<T>(key, out var data))
            {
                this.playerClient.SetData(key, data);
            }
        }

        public bool LoadData(out T data)
        {
            var prefsKey = this.PrefsKey;
            return PlayerPreferences.TryLoad(prefsKey, out data);
        }

        public void SaveData(T data)
        {
            var prefsKey = this.PrefsKey;
            PlayerPreferences.Save(prefsKey, data);
            this.playerClient.SetData(prefsKey, data);
        }

        public void ClearData()
        {
            var prefsKey = this.PrefsKey;
            PlayerPreferences.Remove(prefsKey);
            this.playerClient.RemoveData(prefsKey);
        }
    }
}