using Services;
using UnityEngine;

namespace Game.App
{
    public sealed class RepositorySynchronizer
    {
        private PlayerClient playerClient;

        private IRepository[] repositories;

        private RealtimePreferences realtimePrefs; 
        
        [ServiceInject]
        public void Construct(PlayerClient playerClient, RealtimePreferences realtimePrefs, IRepository[] repositories)
        {
            this.playerClient = playerClient;
            this.realtimePrefs = realtimePrefs;
            this.repositories = repositories;
        }
        
        public void SyncRepositories()
        {
            if (!this.playerClient.IsAuthorized())
            {
                return;
            }
            
            if (!this.realtimePrefs.LoadData(out var realtimeData))
            {
                this.SyncPrefs();
                return;
            }

            var clientLastTime = this.playerClient.LastTime;
            var localLastTime = realtimeData.nowSeconds;

            if (clientLastTime == localLastTime)
            {
                return;
            }
            
            if (clientLastTime > localLastTime)
            {
                this.SyncPrefs();
            }
            else
            {
                this.SyncClient();
            }
        }

        private void SyncPrefs()
        {
            Debug.Log("SYNC PREFS");
            foreach (var repository in this.repositories)
            {
                repository.SynchronizePrefs();
            }
        }

        private void SyncClient()
        {
            Debug.Log("SYNC CLIENT");
            foreach (var repository in this.repositories)
            {
                repository.SynchronizeClient();
            }
        }
    }
}