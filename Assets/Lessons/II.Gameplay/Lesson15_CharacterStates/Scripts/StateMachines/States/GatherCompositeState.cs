using System;
using Declarative;
using Lessons.Character.Model;

namespace Lessons.StateMachines.States
{
    [Serializable]
    public sealed class GatherCompositeState : CompositeState
    {
        public HarvestState harvestState;

        [Construct]
        public void ConstructSelf()
        {
            SetStates(harvestState);
        }
        
        [Construct]
        public void ConstructSubStates(CharacterGathering gathering, CharacterVisual visual)
        {
            harvestState.Construct(gathering.duration, gathering.onComplete);
        }
    }
}