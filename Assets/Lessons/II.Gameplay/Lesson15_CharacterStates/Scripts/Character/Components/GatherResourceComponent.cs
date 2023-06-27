using Lessons.StateMachines.States;
using Lessons.Utils;

namespace Lessons.Character.Components
{
    public sealed class GatherResourceComponent
    {
        private readonly AtomicEvent<ResourceObject> onRequest;

        public GatherResourceComponent(AtomicEvent<ResourceObject> onRequest)
        {
            this.onRequest = onRequest;
        }

        public void StartGather(ResourceObject resourceObject)
        {
            this.onRequest.Invoke(resourceObject);
        }
    }
}