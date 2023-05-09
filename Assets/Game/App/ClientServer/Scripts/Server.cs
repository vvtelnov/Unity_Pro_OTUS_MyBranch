using System;
using System.Text;
using System.Threading.Tasks;
using Asyncoroutine;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace Game.App
{
    public sealed class Server
    {
        public const string SERVER_URL = "http://localhost:3000/";

        public async Task RequestGet<RES>(string rest, Action<RES> onSuccess, Action<string> onError)
        {
            var url = $"{SERVER_URL}/{rest}";

            using (var request = UnityWebRequest.Get(url))
            {
                await request.SendWebRequest();

                if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
                {
                    onError?.Invoke(request.error);
                }
                else
                {
                    var data = request.downloadHandler.text;
                    var response = JsonConvert.DeserializeObject<RES>(data);
                    onSuccess?.Invoke(response);
                }
            }
        }

        public async Task RequestPost<REQ, RES>(string rest, REQ req, Action<RES> onSuccess, Action<string> onError)
        {
            var url = $"{SERVER_URL}/{rest}";
            var json = JsonConvert.SerializeObject(req);

            using (var request = UnityWebRequest.Post(url, "POST"))
            {
                var bodyRaw = Encoding.UTF8.GetBytes(json);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                await request.SendWebRequest();

                if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
                {
                    onError?.Invoke(request.error);
                }
                else
                {
                    var data = request.downloadHandler.text;
                    var response = JsonConvert.DeserializeObject<RES>(data);
                    onSuccess?.Invoke(response);
                }
            }
        }

        public async Task RequestPut<REQ>(string rest, REQ req, Action onSuccess, Action<string> onError)
        {
            var url = $"{SERVER_URL}/{rest}";
            var json = JsonConvert.SerializeObject(req);

            using (UnityWebRequest request = UnityWebRequest.Put(url, json))
            {
                request.SetRequestHeader("Content-Type", "application/json");

                await request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    onError?.Invoke(request.error);
                }
                else
                {
                    onSuccess?.Invoke();
                }
            }
        }
    }
}