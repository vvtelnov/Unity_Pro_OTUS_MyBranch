using Atomic.Elements;
using Atomic.Extensions;
using UnityEngine;

namespace Lessons.Lesson_SectionAndVisuals
{
    public class FireAPI
    {
        [Contract(typeof(AtomicEvent))]
        public const string FIRE_REQUEST = nameof(FIRE_REQUEST);
        
        [Contract(typeof(AtomicEvent))]
        public const string REQUESTED_FIRE_EVENT = nameof(REQUESTED_FIRE_EVENT);
    }

    public class HealthAPI
    {
        [Contract(typeof(AtomicEvent<int>))]
        public const string TAKE_DAMAGE_EVENT = nameof(TAKE_DAMAGE_EVENT);
    }

    public class MoveAPI
    {
        [Contract(typeof(AtomicVariable<Vector3>))]
        public const string MOVE_ACTION = nameof(MOVE_ACTION);
    }
}