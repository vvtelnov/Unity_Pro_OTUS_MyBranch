using System;
using System.Collections.Generic;
using Services;

namespace Game.App
{
    public sealed class UserAuthenticator
    {
        public string UserId { get; private set; }
        
        public string UserPassword { get; private set; }
        
        public string UserToken { get; private set; }

        private Server server;

        private UserRepository repository;

        [ServiceInject]
        public void Construct(Client client, Server server, UserRepository repository)
        {
            this.repository = repository;
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
                    this.UserId = id;
                    this.UserPassword = password;
                    this.UserToken = response.token;
                    onSuccess?.Invoke();
                },
                onError: _ =>
                {
                    onError?.Invoke();
                });
        }
        
        public async void SignUp(Action onSuccess, Action onError)
        {
            await this.server.RequestGet<SignUpResponse>("signUp",
                onSuccess : response =>
                {
                    this.UserId = response.userId;
                    this.UserPassword = response.password;
                    this.UserToken = response.token;

                    this.repository.SaveUser(new UserData
                    {
                        id = response.userId,
                        password = response.password
                    });
                    
                    onSuccess?.Invoke();
                    
                },
                onError: _ =>
                {
                    onError?.Invoke();
                });
        }
    }
}