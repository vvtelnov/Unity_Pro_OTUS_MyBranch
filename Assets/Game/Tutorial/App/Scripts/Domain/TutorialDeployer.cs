using System.Threading.Tasks;
using Game.App;
using Game.GameEngine.GUI;
using GameSystem;
using Services;
using UnityEngine;

namespace Game.Tutorial.App
{
    public sealed class TutorialDeployer : IAppInitListener
    {
        private const string ENGINE_NAME = "Tutorial";

        private TutorialAssetSupplier assetSupplier;

        private GameContainer gameContainer;

        void IAppInitListener.Init()
        {
            this.assetSupplier = ServiceLocator.GetService<TutorialAssetSupplier>();
            this.gameContainer = ServiceLocator.GetService<GameContainer>();
        }

        public async Task DeployTutorial()
        {
            var manager = TutorialManager.Instance;
            if (manager.IsCompleted)
            {
                return;
            }

            await this.InstallEngine();
            await this.InstallInterface();
        }

        private async Task InstallEngine()
        {
            var prefab = await this.assetSupplier.LoadTutorialEngine();
            var engine = GameObject.Instantiate(prefab);
            engine.name = ENGINE_NAME;
            
            var gameElement = engine.GetComponent<IGameElementGroup>();
            var gameService = engine.GetComponent<IGameServiceGroup>();

            this.gameContainer.RegisterElement(gameElement);
            this.gameContainer.RegisterService(gameService);
        }

        private async Task InstallInterface()
        {
            var prefab = await this.assetSupplier.LoadTutorialInterface();

            var canvasService = this.gameContainer.GetService<GUICanvasService>();
            var gui = GameObject.Instantiate(prefab, canvasService.RootTransform);
            gui.name = prefab.name;
            
            var gameElement = gui.GetComponent<IGameElementGroup>();
            var gameService = gui.GetComponent<IGameServiceGroup>();

            this.gameContainer.RegisterElement(gameElement);
            this.gameContainer.RegisterService(gameService);
        }
    }
}