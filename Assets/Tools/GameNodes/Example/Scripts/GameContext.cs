using System;
using System.Collections.Generic;

namespace GameNodes
{
    public sealed class GameContext : GameNode
    {
        private void Awake()
        {
            this.Install();
        }

        private void OnEnable()
        {
            this.Call<GameInit>();
            this.Call<GameStart>();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                this.Call<GamePause>();
            }
            else
            {
                this.Call<GameResume>();
            }
        }

        private void OnDisable()
        {
            this.Call<GameFinish>();
        }

        // public async void Start()
        // {
        //     await this.InstallAsync();
        //     Debug.Log("Installed Async!");
        //     await this.CallAsync<GameInit>();
        //     Debug.Log("Inited Game!");
        //     await this.CallAsync<GameStart>();
        //     Debug.Log("Started Game!");
        // }

        protected override IEnumerable<object> ProvideServices()
        {
            yield return new InputSystem();
        }
    }
}