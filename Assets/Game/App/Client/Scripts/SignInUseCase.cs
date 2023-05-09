using System.Threading.Tasks;

namespace Game.App
{
    public sealed class SignInUseCase
    {
        
        
        
        public struct Request
        {
            public string userId;
            public string password;
        }
        
        public struct Response
        {
            
        }

        public async Task<Response> SignIn(Request request)
        {
            var url = SERVER_URL + "/signIn";

            var json = new StringBuilder()
                .AppendLine("{")
                .AppendLine($"\"userId\" : {userId}")
                .AppendLine($"\\")
                .AppendLine("}");


            var json = $"   {userId}"


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
                    onSuccess?.Invoke(request.downloadHandler.text);
                }
            }


            string param1 = "value1";
            string param2 = "value2";

            var url = SERVER_URL + "?param1=" + param1 + "&param2=" + param2;

            UnityWebRequest request = UnityWebRequest.Get(url + "?param1=" + param1 + "&param2=" + param2);


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
    }
}