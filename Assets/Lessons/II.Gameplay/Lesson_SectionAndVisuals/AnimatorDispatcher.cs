using Lessons.Lesson_Components;
using UnityEngine;

namespace Lessons.Lesson_SectionAndVisuals
{
    public class AnimatorDispatcher : MonoBehaviour
    {
        [SerializeField] private Character _character;
        
        public void ReceiveEvent(string value)
        {
            if (value == "shoot")
            {
                _character.FireEvent.Invoke(); 
            }
        }
    }
}