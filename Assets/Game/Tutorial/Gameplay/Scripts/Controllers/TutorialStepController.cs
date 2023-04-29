using GameSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Tutorial.Gameplay
{
    public abstract class TutorialStepController : MonoBehaviour,
        IGameConstructElement,
        IGameReadyElement,
        IGameStartElement,
        IGameFinishElement
    {
        [FormerlySerializedAs("step")]
        [SerializeField]
        private TutorialStepType stepType;

        private TutorialManager tutorialManager;

        public virtual void ConstructGame(GameContext context)
        {
            this.tutorialManager = TutorialManager.Instance;
        }

        public virtual void ReadyGame()
        {
            this.tutorialManager.OnStepFinished += this.CheckForFinish;
            this.tutorialManager.OnNextStep += this.CheckForStart;
        }

        public virtual void StartGame()
        {
            var stepFinished = this.tutorialManager.IsStepPassed(this.stepType);
            if (!stepFinished)
            {
                this.CheckForStart(this.tutorialManager.CurrentStep);
            }
        }

        public virtual void FinishGame()
        {
            this.tutorialManager.OnStepFinished -= this.CheckForFinish;
            this.tutorialManager.OnNextStep -= this.CheckForStart;
        }

        protected virtual void OnStart()
        {
        }

        protected virtual void OnStop()
        {
        }

        protected void NotifyAboutComplete()
        {
            if (this.tutorialManager.CurrentStep == this.stepType)
            {
                this.tutorialManager.FinishCurrentStep();
            }
        }

        protected void NotifyAboutMoveNext()
        {
            if (this.tutorialManager.CurrentStep == this.stepType)
            {
                this.tutorialManager.MoveToNextStep();
            }
        }

        protected void NotifyAboutCompleteAndMoveNext()
        {
            if (this.tutorialManager.CurrentStep == this.stepType)
            {
                this.tutorialManager.FinishCurrentStep();
                this.tutorialManager.MoveToNextStep();
            }
        }

        protected bool IsStepFinished()
        {
            return this.tutorialManager.IsStepPassed(this.stepType);
        }

        private void CheckForFinish(TutorialStepType step)
        {
            if (this.stepType == step)
            {
                this.OnStop();
            }
        }

        private void CheckForStart(TutorialStepType step)
        {
            if (this.stepType == step)
            {
                this.OnStart();
            }
        }
    }
}