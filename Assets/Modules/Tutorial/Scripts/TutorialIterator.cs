using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class TutorialIterator<T>
    {
        public event Action<T> OnStepFinished;

        public event Action<T> OnNextStep;

        public event Action OnCompleted;

        public bool IsCompleted
        {
            get { return this.isCompleted; }
        }

        public T CurrentStep
        {
            get { return this.stageList[this.currentIndex]; }
        }

        private readonly List<T> stageList;

        private int currentIndex;

        private bool isCompleted;

        public TutorialIterator(List<T> stages)
        {
            this.currentIndex = 0;
            this.isCompleted = false;
            this.stageList = new List<T>(stages);
        }

        public void SetAsCompleted()
        {
            this.isCompleted = true;
            this.currentIndex = this.stageList.Count - 1;
        }

        public void SetOnStep(int stepIndex)
        {
            this.isCompleted = false;
            this.currentIndex = Mathf.Clamp(stepIndex, 0, this.stageList.Count - 1);
        }

        public void FinishCurrentStep()
        {
            if (this.isCompleted)
            {
                return;
            }

            this.OnStepFinished?.Invoke(this.CurrentStep);
        }

        public void MoveToNextStep()
        {
            if (this.isCompleted)
            {
                return;
            }

            if (this.currentIndex >= this.stageList.Count - 1)
            {
                this.isCompleted = true;
                this.OnCompleted?.Invoke();
                return;
            }

            this.currentIndex++;
            this.OnNextStep?.Invoke(this.CurrentStep);
        }

        public bool IsStepPassed(T step)
        {
            if (this.isCompleted)
            {
                return true;
            }

            var index = this.stageList.IndexOf(step);
            return index < this.currentIndex;
        }

        public int IndexOfStep(T step)
        {
            return this.stageList.IndexOf(step);
        }
    }
}