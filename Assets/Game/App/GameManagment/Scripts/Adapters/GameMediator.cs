using Newtonsoft.Json;
using Services;

namespace Game.App
{
    public abstract class GameMediator<TData, TService> : IGameMediator
    {
        [ServiceInject]
        private GameFacade gameFacade;
        
        void IGameMediator.SetupData(GameRepository repository)
        {
            var service = this.gameFacade.GetService<TService>();

            if (repository.TryGetData(nameof(TData), out var json))
            {
                var data = JsonConvert.DeserializeObject<TData>(json);
                this.SetupFromData(service, data);
            }
            else
            {
                this.SetupByDefault(service);
            }
        }

        void IGameMediator.SaveData(GameRepository repository)
        {
            var service = this.gameFacade.GetService<TService>();
            var data = this.ConvertToData(service);
            var json = JsonConvert.SerializeObject(data);
            repository.SetData(nameof(TData), json);
        }

        protected abstract void SetupFromData(TService service, TData data);

        protected abstract void SetupByDefault(TService service);

        protected abstract TData ConvertToData(TService service);
    }
}