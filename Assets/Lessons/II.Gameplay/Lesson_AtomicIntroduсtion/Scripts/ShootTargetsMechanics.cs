using Atomic.Elements;
using Lessons.Lesson_Components.Components;
using UnityEngine;

namespace Lessons.Lesson_AtomicIntroduсtion.Scripts
{
    public class ShootTargetsMechanics
    {
        private readonly IAtomicAction _shootAction;
        private readonly IAtomicValue<Vector3> _rootPosition;
        private readonly IAtomicValue<Vector3> _targetPosition;
        private readonly IAtomicValue<float> _radius;

        public ShootTargetsMechanics(
            IAtomicAction shootAction, 
            IAtomicValue<Vector3> rootPosition,
            IAtomicValue<Vector3> targetPosition, 
            IAtomicValue<float> radius)
        {
            _shootAction = shootAction;
            _rootPosition = rootPosition;
            _targetPosition = targetPosition;
            _radius = radius;
        }

        public void Update()
        {
            var direction = _targetPosition.Value - _rootPosition.Value;
            
            if (direction.magnitude < _radius.Value)
            {
                _shootAction.Invoke();
            }
        }
    }
}