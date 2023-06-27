using System;
using Lessons.Utils;

namespace Lessons.StateMachines.States
{
    [Serializable]
    public sealed class HarvestState : UpdateState
    {
        private float currentTime;

        private AtomicVariable<float> duration;
        private AtomicEvent onComplete;

        public void Construct(
            AtomicVariable<float> duration,
            AtomicEvent onComplete
        )
        {
            this.duration = duration;
            this.onComplete = onComplete;
        }

        protected override void OnEnter()
        {
            this.currentTime = 0.0f;
        }

        protected override void OnUpdate(float deltaTime)
        {
            this.currentTime += deltaTime;
            if (this.currentTime >= this.duration)
            {
                this.onComplete.Invoke();
            }
        }
    }
}