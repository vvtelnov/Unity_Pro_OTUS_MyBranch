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

        public string UserId { get; set; }

        public string Token { get; set; }

        public long LastTime { get; set; } 
        
        private Dictionary<string, object> playerData = new();

        public void SetPlayerData(string json)
        {
            var playerData = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            if (playerData != null)
            {
                this.playerData = playerData;
            }
        }

        public string GetPlayerData()
        {
            return JsonConvert.SerializeObject(this.playerData);
        }

        public void SetPlayerValue(string key, object value)
        {
            this.playerData[key] = value;
        }

        public object GetPlayerValue(string key)
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