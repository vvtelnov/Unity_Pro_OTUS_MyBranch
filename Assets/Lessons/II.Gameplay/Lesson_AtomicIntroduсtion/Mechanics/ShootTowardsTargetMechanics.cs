using Atomic.Elements;
using UnityEngine;

namespace Lessons.Lesson_AtomicIntroduсtion.Mechanics
{
    public class ShootTowardsTargetMechanics
    {
        private readonly IAtomicValue<Vector3> _myPosition;
        private readonly IAtomicValue<Vector3> _targetPosition;
        private readonly IAtomicAction _shootAction;
        
        private readonly IAtomicValue<float> _radius;
        private readonly IAtomicValue<bool> _enabled;

        public ShootTowardsTargetMechanics(
            IAtomicValue<Vector3> myPosition, 
            IAtomicValue<Vector3> targetPosition, 
            IAtomicAction shootAction, 
            IAtomicValue<float> radius, 
            IAtomicValue<bool> enabled)
        {
            _myPosition = myPosition;
            _targetPosition = targetPosition;
            _shootAction = shootAction;
            _radius = radius;
            _enabled = enabled;
        }

        public void Update()
        {
            if (!_enabled.Value)
            {
                return;
            }
            
            var direction = _targetPosition.Value - _myPosition.Value;
            
            if (direction.magnitude < _radius.Value)
            {
                _shootAction.Invoke();
            }
        }
    }
}