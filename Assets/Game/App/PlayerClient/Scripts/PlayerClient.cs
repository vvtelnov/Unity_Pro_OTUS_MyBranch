using System.Collections.Generic;

namespace Game.App
{
    public sealed class PlayerClient
    {
        public string UserId { get; set; }

        public string Token { get; set; }

        public long LastTime { get; set; }

        private Dictionary<string, object> playerState = new();

        public T GetData<T>(string key)
        {
            return (T) this.playerState[key];
        }

        public bool TryGetData<T>(string key, out T tValue)
        {
            if (this.playerState.TryGetValue(key, out var value))
            {
                tValue = (T) value;
                return true;
            }

            tValue = default;
            return false;
        }

        public object GetData(string key)
        {
            return this.playerState[key];
        }

        public bool TryGetData(string key, out object value)
        {
            return this.playerState.TryGetValue(key, out value);
        }

        public void SetData<T>(string key, T value)
        {
            this.playerState[key] = value;
        }

        public void SetData(string key, object value)
        {
            this.playerState[key] = value;
        }

        public bool ContainsData(string key)
        {
            return this.playerState.ContainsKey(key);
        }

        public void RemoveData(string key)
        {
            this.playerState.Remove(key);
        }

        internal void SetPlayerState(Dictionary<string, object> playerData)
        {
            this.playerState = playerData;
        }

        internal Dictionary<string, object> GetPlayerState()
        {
            return new Dictionary<string, object>(this.playerState);
        }

        internal bool IsAuthorized()
        {
            return this.UserId != null && this.Token != null;
        }
    }
}