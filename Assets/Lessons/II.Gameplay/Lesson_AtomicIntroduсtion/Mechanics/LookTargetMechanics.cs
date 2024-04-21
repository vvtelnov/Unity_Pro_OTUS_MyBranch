using Atomic.Elements;
using Lessons.Lesson_Components.Components;
using UnityEngine;

namespace Lessons.Lesson_AtomicIntroduсtion.Mechanics
{
    public class LookTargetMechanics
    {
        private readonly Transform _root;
        private readonly IAtomicValue<Vector3> _targetPoint;
        private readonly RotationComponent _rotationComponent;
        private readonly IAtomicValue<bool> _enabled;
        
        public LookTargetMechanics(Transform root,
            IAtomicValue<Vector3> targetPoint,
            RotationComponent rotationComponent, 
            IAtomicValue<bool> enabled)
        {
            _root = root;
            _targetPoint = targetPoint;
            _rotationComponent = rotationComponent;
            _enabled = enabled;
        }

        public void Update()
        {
            if (!_enabled.Value)
            {
                return;
            }
            
            var direction = _targetPoint.Value - _root.position;
            _rotationComponent.Rotate(direction);  
        }
    }
}