using System.Collections.Generic;
using Lessons.Architecture.GameSystem;
using Newtonsoft.Json;
using UnityEngine;

namespace Lessons.Architecture.SaveLoad
{
    public sealed class GameRepository : IGameRepository
    {
        private const string STATE_KEY = "Lesson/GameState";
        
        private Dictionary<string, string> gameState = new();

        public string GetData(string key)
        {
            return this.gameState[key];
        }

        public bool TryGetData(string key, out string value)
        {
            return this.gameState.TryGetValue(key, out value);
        }

        public void SetData(string key, string value)
        {
            this.gameState[key] = value;
        }

        public void LoadState()
        {
            if (PlayerPrefs.HasKey(STATE_KEY))
            {
                string serialziedGameState = PlayerPrefs.GetString(STATE_KEY);
                this.gameState = JsonConvert.DeserializeObject<Dictionary<string, string>>(serialziedGameState);    
            }
            else
            {
                this.gameState = new Dictionary<string, string>();
            }
        }

        public void SaveState()
        {
            var serialziedGameState = JsonConvert.SerializeObject(this.gameState);
            PlayerPrefs.SetString(STATE_KEY, serialziedGameState);
        }
    }
}