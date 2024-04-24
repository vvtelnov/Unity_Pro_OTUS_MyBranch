using Atomic.Elements;
using Atomic.Extensions;
using UnityEngine;

namespace Lessons.Lesson_SectionAndVisuals
{
    public class ShootAPI
    {
        [Contract(typeof(AtomicEvent))]
        public const string SHOOT_REQUEST = nameof(SHOOT_REQUEST);
        
        [Contract(typeof(AtomicEvent))]
        public const string SHOOT_ACTION = nameof(SHOOT_ACTION);
    }

    public class HealthAPI
    {
        [Contract(typeof(AtomicEvent<int>))]
        public const string TAKE_DAMAGE_ACTION = nameof(TAKE_DAMAGE_ACTION);
    }

    public class MoveAPI
    {
        [Contract(typeof(AtomicVariable<Vector3>))]
        public const string MOVE_DIRECTION = nameof(MOVE_DIRECTION);
    }
}