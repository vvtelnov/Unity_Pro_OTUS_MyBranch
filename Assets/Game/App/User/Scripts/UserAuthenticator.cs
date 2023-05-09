using System;
using Services;

namespace Game.App
{
    public sealed class UserAuthenticator
    {
        public bool IsAuthorized { get; private set; }
        
        public string Id { get; private set; }
        
        public string Password { get; private set; }
        
        public string Token { get; private set; }

        private BackendServer server;

        private UserRepository repository;

        [ServiceInject]
        public void Construct(UserRepository repository, BackendServer server)
        {
            this.repository = repository;
            this.server = server;
        }

        public void Authenticate(Action onSuccess, Action onError)
        {
            if (this.repository.LoadUser(out var data))
            {
                this.SignIn(data.id, data.password, onSuccess, onError);
            }
            else
            {
                this.SignUp(onSuccess, onError);
            }
        }

        private async void SignIn(string id, string password, Action onSuccess, Action onError)
        {
            var request = new SignInRequest
            {
                userId = id,
                password = password
            };
            
            await this.server.RequestPost<SignInRequest, SignInResponse>("signIn", request,
                onSuccess: response =>
                {
                    this.Id = id;
                    this.Password = password;
                    this.Token = response.token;
                    this.IsAuthorized = true;
                    onSuccess?.Invoke();
                },
                onError: _ =>
                {
                    this.IsAuthorized = false;
                    onError?.Invoke();
                });
        }
        
        public async void SignUp(Action onSuccess, Action onError)
        {
            await this.server.RequestGet<SignUpResponse>("signUp",
                onSuccess : response =>
                {
                    this.Id = response.userId;
                    this.Password = response.password;
                    this.Token = response.token;
                    this.IsAuthorized = true;

                    this.repository.SaveUser(new UserData
                    {
                        id = response.userId,
                        password = response.password
                    });
                    
                    onSuccess?.Invoke();
                },
                onError: _ =>
                {
                    this.IsAuthorized = false;
                    onError?.Invoke();
                });
        }

        private struct SignInRequest
        {
            public string userId;
            public string password;
        }
        
        private struct SignInResponse
        {
            public string token;
        }

        private struct SignUpResponse
        {
            public string userId;
            public string password;
            public string token;
        }
    }
}