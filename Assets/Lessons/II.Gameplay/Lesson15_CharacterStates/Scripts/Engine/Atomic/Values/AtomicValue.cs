using System;
using Sirenix.OdinInspector;

namespace Lessons.Gameplay.States
{
    public sealed class AtomicValue<T>
    {
        [ShowInInspector, ReadOnly]
        public T Value => this.func.Invoke();

        private readonly Func<T> func;

        public AtomicValue(Func<T> func)
        {
            this.func = func;
        }
    }
}