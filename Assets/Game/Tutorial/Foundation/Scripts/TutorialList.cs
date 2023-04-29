using System.Collections.Generic;
using UnityEngine;

namespace Game.Tutorial
{
    [CreateAssetMenu(
        fileName = "TutorialList",
        menuName = "Tutorial/New TutorialList",
        order = 35
    )]
    public sealed class TutorialList : ScriptableObject
    {
        [SerializeField]
        private List<TutorialStepType> steps = new();

        public int IndexOf(TutorialStepType step)
        {
            return this.steps.IndexOf(step);
        }

        public List<TutorialStepType> GetStepList()
        {
            return this.steps;
        }
    }
}