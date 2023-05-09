namespace Game.App
{
    public class Client
    {
        public string UserId
        {
            get { return this.userId; }
        }
        
        public string Token
        {
            get { return this.token; }
        }

        private string userId;

        private string token;
        
        public void UpdateUser(string userId)
        {
            this.userId = userId;
        }

        public void UpdateToken(string token)
        {
            this.token = token;
        }


        //
        // [ServiceInject]
        // public void Construct(Server server)
        // {
        //     this.server = server;
        // }
        //
        // public 
        //
        //
        // public async void SignIn(SignInRequest request, Action onSuccess, Action onError)
        // {
        //     await this.server.RequestPost<SignInRequest, SignInResponse>(
        //         "signIn",
        //         request,
        //         onSuccess: res =>
        //         {
        //             this.token = res.token;
        //             onSuccess?.Invoke();
        //         },
        //         onError: err => onError?.Invoke()
        //     );
        // }
        //
        // public async void SignUp(Action<SignUpResponse> onSuccess, Action<string> onError)
        // {
        //     await this.server.RequestGet<SignUpResponse>("signUp", onSuccess, onError);
        // }
        //
        // public async void LoadPlayer(string token, Action<Dictionary<string, object>> onSucces, Action<string> onError)
        // {
        //     await this.server.RequestGet("save_player?userId=1&token=sfercjewxeing57")
        // }
        //
        // public
    }
}