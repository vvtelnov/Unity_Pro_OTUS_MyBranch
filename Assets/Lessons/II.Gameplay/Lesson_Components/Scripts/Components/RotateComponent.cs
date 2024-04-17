using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class RotateComponent : MonoBehaviour, IRotateComponent
    {
        [SerializeField] private Transform _root;
        [SerializeField] private float _rotateRate;
        [SerializeField] private bool _canRotate;
        
        public void Rotate(Vector3 forwardDirection)
        {
            if (!_canRotate)
            {
                return;
            }

            var targetRotation = Quaternion.LookRotation(forwardDirection, Vector3.up);
            _root.rotation = Quaternion.Lerp(_root.rotation, targetRotation, _rotateRate);
        }
    }
}