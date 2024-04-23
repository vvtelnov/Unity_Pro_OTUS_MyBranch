using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Lessons.Lesson_SectionAndVisuals
{
    public class AnimatorDispatcher : MonoBehaviour
    {
        [SerializeField] private AtomicEntity _character;
        
        public void ReceiveEvent(string value)
        {
            if (value == "shoot")
            {
                _character.Get<IAtomicEvent>(FireAPI.REQUESTED_FIRE_EVENT).Invoke();
            }
        }
    }
}