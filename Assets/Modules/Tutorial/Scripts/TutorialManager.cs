using System;
using UnityEngine;

namespace Tutorial
{
    public abstract class TutorialManager<T> : MonoBehaviour
    {
        public event Action OnInitialized;

        public event Action<T> OnStepFinished
        {
            add { this.Iterator.OnStepFinished += value; }
            remove { this.Iterator.OnStepFinished -= value; }
        }

        public event Action<T> OnNextStep
        {
            add { this.Iterator.OnNextStep += value; }
            remove { this.Iterator.OnNextStep -= value; }
        }

        public event Action OnCompleted
        {
            add { this.Iterator.OnCompleted += value; }
            remove { this.Iterator.OnCompleted -= value; }
        }

        public bool IsInitialized { get; private set; }

        public bool IsCompleted
        {
            get { return this.Iterator.IsCompleted; }
        }

        public T CurrentStep
        {
            get { return this.Iterator.CurrentStep; }
        }

        protected abstract TutorialIterator<T> Iterator { get; }

        public void InitAsCompleted()
        {
            this.Iterator.SetAsCompleted();
            this.IsInitialized = true;
            this.OnInitialized?.Invoke();
        }

        public void InitOnStep(int index = 0)
        {
            this.Iterator.SetOnStep(index);
            this.IsInitialized = true;
            this.OnInitialized?.Invoke();
        }

        public void FinishCurrentStep()
        {
            this.Iterator.FinishCurrentStep();
        }

        public void MoveToNextStep()
        {
            this.Iterator.MoveToNextStep();
        }
        
        public bool IsStepPassed(T step)
        {
            return this.Iterator.IsStepPassed(step);
        }

        public int IndexOfStep(T step)
        {
            return this.Iterator.IndexOfStep(step);
        }
    }
}