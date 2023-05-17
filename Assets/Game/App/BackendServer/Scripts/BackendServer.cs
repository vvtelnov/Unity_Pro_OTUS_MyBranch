using System.Text;
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

        public UnityWebRequest Get(string route)
        {
            var url = this.CombineUrl(route);
            return UnityWebRequest.Get(url);
        }

        public UnityWebRequest Post(string route, string bodyJson)
        {
            var url = this.CombineUrl(route);
            var request = UnityWebRequest.Post(url, "POST");
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(bodyJson));
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            return request;
        }

        public UnityWebRequest Put(string route, string data)
        {
            var url = this.CombineUrl(route);
            var request = UnityWebRequest.Put(url, data);
            request.SetRequestHeader("Content-Type", "application/json");
            return request;
        }

        private string CombineUrl(string route)
        {
            return $"{this.url}:{this.port}/{route}";
        }
    }
}