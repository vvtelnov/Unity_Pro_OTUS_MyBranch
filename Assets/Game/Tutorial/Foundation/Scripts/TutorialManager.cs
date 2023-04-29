using System;
using Tutorial;
using UnityEngine;

namespace Game.Tutorial
{
    [AddComponentMenu("Tutorial/Tutorial Manager")]
    public sealed class TutorialManager : TutorialManager<TutorialStepType>
    {
        internal static TutorialManager Instance { get; private set; }

        protected override TutorialIterator<TutorialStepType> Iterator
        {
            get { return iterator; }
        }

        private TutorialIterator<TutorialStepType> iterator;

        [SerializeField]
        private TutorialList config;

        private void Awake()
        {
            if (Instance != null)
            {
                throw new Exception("TutorialManager is already created!");
            }

            var steps = this.config.GetStepList();
            this.iterator = new TutorialIterator<TutorialStepType>(steps);
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }
    }
}