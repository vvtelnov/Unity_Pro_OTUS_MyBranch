using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class ShootController : MonoBehaviour
    {
        [SerializeField] private FireComponent _fireComponent;
        
        private void Update()
        {
            _fireComponent.FireRequest = Input.GetKey(KeyCode.Space);
        }
    }
}