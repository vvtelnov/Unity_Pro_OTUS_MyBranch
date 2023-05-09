using System;
using System.Text;
using System.Threading.Tasks;
using Asyncoroutine;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace Game.App
{
    public sealed class BackendServer
    {
        private readonly string url;

        private readonly int port;

        public BackendServer(string url, int port)
        {
            this.url = url;
            this.port = port;
        }

        public async Task RequestGet<RES>(string rest, Action<RES> onSuccess, Action<string> onError)
        {
            var url = this.CombineUrl(rest);

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

        public async Task RequestGet(string rest, Action<string> onSuccess, Action<string> onError)
        {
            var url = this.CombineUrl(rest);

            using (var request = UnityWebRequest.Get(url))
            {
                await request.SendWebRequest();

                if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
                {
                    onError?.Invoke(request.error);
                }
                else
                {
                    onSuccess?.Invoke(request.downloadHandler.text);
                }
            }
        }

        public async Task RequestPost<REQ, RES>(string rest, REQ req, Action<RES> onSuccess, Action<string> onError)
        {
            var url = this.CombineUrl(rest);
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
            var url = this.CombineUrl(rest);
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
        
        public async Task RequestPut(string rest, string json, Action onSuccess, Action<string> onError)
        {
            var url = this.CombineUrl(rest);

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

        private string CombineUrl(string rest)
        {
            return $"{this.url}:{this.port}/{rest}";
        }
    }
}