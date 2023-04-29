using Game.App;
using Services;

namespace Game.Tutorial.App
{
    public class TutorialMediator :
        IAppInitListener,
        IAppStartListener,
        IAppQuitListener
    {
        [ServiceInject]
        private TutorialRepository repository;

        [ServiceInject]
        private TutorialManager tutorialManager;

        void IAppInitListener.Init()
        {
            this.LoadTutorialState();
        }

        void IAppStartListener.Start()
        {
            this.tutorialManager.OnStepFinished += this.OnTutorialStepFinished;
            this.tutorialManager.OnCompleted += this.OnTutorialCompleted;
        }

        void IAppQuitListener.OnQuit()
        {
            this.tutorialManager.OnStepFinished -= this.OnTutorialStepFinished;
            this.tutorialManager.OnCompleted -= this.OnTutorialCompleted;
        }

        private void LoadTutorialState()
        {
            if (!this.LoadData(out var data))
            {
                this.tutorialManager.InitOnStep(index: 0);
            }
            else if (!data.isCompleted)
            {
                this.tutorialManager.InitOnStep(data.stepIndex);
            }
            else
            {
                this.tutorialManager.InitAsCompleted();
            }
        }

        protected virtual bool LoadData(out TutorialData data)
        {
            return this.repository.LoadState(out data);
        }

        private void OnTutorialStepFinished(TutorialStepType step)
        {
            var nextStepIndex = this.tutorialManager.IndexOfStep(step) + 1;

            var data = new TutorialData
            {
                isCompleted = false,
                stepIndex = nextStepIndex
            };

            this.repository.SaveState(data);
        }

        private void OnTutorialCompleted()
        {
            var data = new TutorialData
            {
                isCompleted = true,
                stepIndex = this.tutorialManager.IndexOfStep(this.tutorialManager.CurrentStep)
            };

            this.repository.SaveState(data);
        }
    }
}