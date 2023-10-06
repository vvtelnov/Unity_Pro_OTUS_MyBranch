using System;
using System.Threading.Tasks;
using Asyncoroutine;
using Newtonsoft.Json;
using UnityEngine;
using static UnityEngine.Networking.UnityWebRequest.Result;

// ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable UnusedParameter.Local

namespace Game.App
{
    public sealed class GameClient
    {
        private const string USER_KEY = "UserData";

        public bool IsAuthorized
        {
            get { return this.isAuthorized; }
        }

        private readonly GameServer server;

        private string userId;
        private string token;
        private bool isAuthorized;

        public GameClient(GameServer server)
        {
            this.server = server;
        }

        public Task<bool> Authenticate()
        {
            if (ES3.KeyExists(USER_KEY))
            {
                var user = ES3.Load<UserData>(USER_KEY);
                return this.SignIn(user.id, user.password);
            }

            return this.SignUp();
        }

        public async Task<bool> SignIn(string userId, string password)
        {
            var body = new SignInRequest
            {
                userId = userId,
                password = password
            };

            using (var request = this.server.Post("signIn", body))
            {
                Debug.Log($"SIGN IN {userId} {password}");
                await request.SendWebRequest();

                if (request.result is ConnectionError or ProtocolError)
                {
                    return false;
                }

                var responseJson = request.downloadHandler.text;
                if (responseJson == null)
                {
                    return false;
                }

                var response = JsonConvert.DeserializeObject<SignInResponse>(responseJson);

                this.userId = userId;
                this.token = response.token;

                ES3.Save(USER_KEY, new UserData
                {
                    id = userId,
                    password = password
                });

                return true;
            }
        }

        public async Task<bool> SignUp()
        {
            //TODO: Убрать device id
            var deviceId = SystemInfo.deviceUniqueIdentifier;

            //TODO: Использовать рекламный идентификатор
            // Application.RequestAdvertisingIdentifierAsync();

            using (var request = this.server.Get($"signUp/?deviceId={deviceId}"))
            {
                Debug.Log($"SIGN UP {deviceId}");
                await request.SendWebRequest();

                if (request.result is ConnectionError or ProtocolError)
                {
                    return false;
                }

                var responseJson = request.downloadHandler.text;
                if (responseJson == null)
                {
                    return false;
                }

                var response = JsonConvert.DeserializeObject<SignUpResponse>(responseJson);

                var userId = response.userId;
                var password = response.password;
                var token = response.token;

                this.userId = userId;
                this.token = token;

                ES3.Save(USER_KEY, new UserData
                {
                    id = userId,
                    password = password
                });

                return true;
            }
        }
        
        public async Task<(bool, string)> GetPlayerData()
        {
            if (!this.IsAuthorized)
            {
                return (false, null);
            }

            var route = $"load_player?userId={this.userId}&token={this.token}";
            using (var request = this.server.Get(route))
            {
                await request.SendWebRequest();

                if (request.result is ConnectionError or ProtocolError)
                {
                    return (false, null);
                }

                var playerState = request.downloadHandler.text;
                if (playerState == "null")
                {
                    return (false, null);
                }

                return (true, playerState);
            }
        }

        public async Task<bool> SetPlayerData(string playerState)
        {
            if (!this.IsAuthorized)
            {
                return false;
            }

            Debug.Log($"UPLOAD DATA {playerState}");

            var route = $"save_player?userId={this.userId}&token={this.token}";
            using (var request = this.server.Put(route, playerState))
            {
                await request.SendWebRequest();
                return request.result == Success;
            }
        }
        
        [Serializable]
        private struct UserData
        {
            [SerializeField]
            public string id;

            [SerializeField]
            public string password;
        }
    }
}