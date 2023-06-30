using Entities;
using Lessons.Utils;

namespace Lessons.Character.Components
{
    public sealed class GatherResourceComponent
    {
        private readonly AtomicEvent<IEntity> onRequest;

        public GatherResourceComponent(AtomicEvent<IEntity> onRequest)
        {
            this.onRequest = onRequest;
        }

        public void StartGather(IEntity resourceObject)
        {
            this.onRequest.Invoke(resourceObject);
        }
    }
}