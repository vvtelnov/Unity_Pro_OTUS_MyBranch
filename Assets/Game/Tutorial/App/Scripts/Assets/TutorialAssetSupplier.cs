using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Tutorial.App
{
    public sealed class TutorialAssetSupplier
    {
        private const string ENGINE_KEY = "TutorialEngine";

        private const string INTERFACE_KEY = "TutorialInterface";

        private const string STEP_CONFIG_PATH = "TutorialStepConfig";

        public TutorialList LoadStepConfig()
        {
            return Resources.Load<TutorialList>(STEP_CONFIG_PATH);
        }

        public Task<GameObject> LoadTutorialEngine()
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(ENGINE_KEY);
            return handle.Task;
        }

        public Task<GameObject> LoadTutorialInterface()
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(INTERFACE_KEY);
            return handle.Task;
        }
    }
}