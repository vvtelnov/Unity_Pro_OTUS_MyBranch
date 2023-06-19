using System;
using Lessons.Gameplay.States;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Engine.Atomic.Values
{
    [Serializable]
    public class AtomicVariable<T> : IAtomicVariable<T>
    {
        public AtomicEvent<T> OnChanged { get; set; } = new();

        public T Value
        {
            get => this.value;
            set => SetValue(value);
        }

        [OnValueChanged("OnValueChanged")]
        [SerializeField]
        private T value;

        public static implicit operator T(AtomicVariable<T> value)
        {
            return value.value;
        }

        public static implicit operator AtomicVariable<T>(T value)
        {
            return new AtomicVariable<T>(value);
        }

        public AtomicVariable()
        {
            this.value = default;
        }

        public AtomicVariable(T value)
        {
            this.value = value;
        }

        protected virtual void SetValue(T value)
        {
            this.value = value;
            OnChanged?.Invoke(value);
        }
        
#if UNITY_EDITOR
        private void OnValueChanged(T value)
        {
            OnChanged?.Invoke(this.value);
        }
#endif
    }
}