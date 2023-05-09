using System;
using System.Collections.Generic;
using Services;

namespace Game.App
{
    //TODO: REMOVE!
    public sealed class Player___Saver
    {
        private BackendServer server;

        private UserAuthenticator userAuth;

        [ServiceInject]
        public void Construct(BackendServer server, UserAuthenticator userAuth)
        {
            this.server = server;
            this.userAuth = userAuth;
        }
        
        public async void SavePlayerState(Dictionary<string, object> data, Action onSuccess, Action onError)
        {
            var url = $"save_player?userId={this.userAuth.Id}&token={this.userAuth.Token}";
            await this.server.RequestPut(url, data, onSuccess, _ => onError?.Invoke());
        }
    }
}