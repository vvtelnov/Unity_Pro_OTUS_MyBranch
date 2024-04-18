using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class ShootController : MonoBehaviour
    {
        [SerializeField] private Character _character;
        
        private void Update()
        {
            _character.FireRequest = Input.GetKey(KeyCode.Space);
        }
    }
}