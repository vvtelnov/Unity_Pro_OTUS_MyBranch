using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Game.App
{
    public sealed class PlayerClient
    {
        public bool IsAuthorized
        {
            get { return this.UserId != null && this.Token != null; }
        }

        public string UserId { get; private set; }

        public string Token { get; private set; }

        private Dictionary<string, object> playerData = new();

        public void SetAuthorized(string userId, string token)
        {
            this.UserId = userId;
            this.Token = token;
        }

        public void SetPlayerData(string playerData)
        {
            this.playerData = JsonConvert.DeserializeObject<Dictionary<string, object>>(playerData);
        }

        public string GetPlayerData()
        {
            return JsonConvert.SerializeObject(this.playerData);
        }

        public void SetValue(string key, object value)
        {
            if (this.playerData == null)
            {
                throw new Exception("Player data is not downloaded!");
            }

            this.playerData[key] = value;
        }

        public object GetValue(string key)
        {
            if (this.playerData == null)
            {
                throw new Exception("Player data is not downloaded!");
            }

            if (!this.playerData.TryGetValue(key, out var value))
            {
                throw new Exception($"Key not found {key}!");
            }

            return value;
        }
    }
}