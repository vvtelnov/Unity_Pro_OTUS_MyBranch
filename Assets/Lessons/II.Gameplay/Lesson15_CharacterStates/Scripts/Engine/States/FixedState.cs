using Declarative;

namespace Lessons.Gameplay.States
{
    public abstract class FixedState : IState, IFixedUpdateListener
    {
        private bool enabled;

        public virtual void Enter()
        {
            this.enabled = true;
        }

        public virtual void Exit()
        {
            this.enabled = false;
        }

        void IFixedUpdateListener.FixedUpdate(float deltaTime)
        {
            if (this.enabled)
            {
                this.FixedUpdate(deltaTime);
            }
        }

        protected abstract void FixedUpdate(float deltaTime);
    }
}