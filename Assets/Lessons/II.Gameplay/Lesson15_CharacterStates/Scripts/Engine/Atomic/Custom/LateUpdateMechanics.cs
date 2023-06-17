using System;
using Declarative;

namespace Lessons.Gameplay.States
{
    public sealed class LateUpdateMechanics : ILateUpdateListener
    {
        private Action<float> action;
        
        public void SetAction(Action<float> action)
        {
            this.action = action;
        }
        
        void ILateUpdateListener.LateUpdate(float deltaTime)
        {
            this.action?.Invoke(deltaTime);
        }
    }
}