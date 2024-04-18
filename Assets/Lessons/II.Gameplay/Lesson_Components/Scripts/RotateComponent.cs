using System;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class RotateComponent : MonoBehaviour
    {
        [SerializeField] private Transform _rotationRoot;
        [SerializeField] private float _rotateRate = 0.03f;
        [SerializeField] private bool _canRotate = true;
        
        private Vector3 _rotateDirection;
        private CompositeCondition _condition = new CompositeCondition();

        private void Update()
        {
            if (!CanRotate())
            {
                return;
            }

            if (_rotateDirection == Vector3.zero)
            {
                return;
            }
            
            var targetRotation = Quaternion.LookRotation(_rotateDirection, Vector3.up);
            _rotationRoot.rotation = Quaternion.Lerp(_rotationRoot.rotation, targetRotation, _rotateRate);
        }

        public void Rotate(Vector3 forwardDirection)
        {
            _rotateDirection = forwardDirection;
        }

        public void AppendCondition(Func<bool> condition)
        {
            _condition.Add(condition);
        }

        public bool CanRotate()
        {
            return _condition.IsTrue() && _canRotate;
        }
    }
}