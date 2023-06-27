using System;
using Declarative;
using Lessons.Character.Model;

namespace Lessons.StateMachines.States
{
    [Serializable]
    public sealed class GatherResourceState : CompositeState
    {
        public AnimatorState animatorState;
        public HarvestState harvestState;

        [Construct]
        public void ConstructSelf()
        {
            SetStates(harvestState, animatorState);
        }
        
        [Construct]
        public void ConstructSubStates(CharacterGathering gathering, CharacterVisual visual)
        {
            animatorState.Construct(visual.animator);
            harvestState.Construct(gathering.duration, gathering.onComplete);
        }
    }
}