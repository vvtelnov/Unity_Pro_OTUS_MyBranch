using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private RotateComponent _rotateComponent;
        
        private void Update()
        {
            var forwardDirection = _target.position - transform.position;
            _rotateComponent.Rotate(forwardDirection);
        }
    }
}