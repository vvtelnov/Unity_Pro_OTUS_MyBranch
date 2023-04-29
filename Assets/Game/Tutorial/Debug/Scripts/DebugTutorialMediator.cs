#if UNITY_EDITOR
using Game.Tutorial.App;
using Services;

namespace Game.Tutorial.Development
{
    public sealed class DebugTutorialMediator : TutorialMediator
    {
        protected override bool LoadData(out TutorialData data)
        {
            var debugConfig = DebugTutorialConfig.Instance;
            if (!debugConfig.isDebug)
            {
                return base.LoadData(out data);                
            }

            var assetSuppier = ServiceLocator.GetService<TutorialAssetSupplier>();
            var stepConfig = assetSuppier.LoadStepConfig();
            
            data = new TutorialData
            {
                isCompleted = debugConfig.isCompleted,
                stepIndex = stepConfig.IndexOf(debugConfig.currentStep)
            };
            return true;
        }
    }
}
#endif