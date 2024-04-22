using Atomic.Elements;
using UnityEngine;

namespace Lessons.Lesson_AtomicIntroduсtion.Scripts
{
    public class LookAtMechanics
    {
        private readonly IAtomicAction<Vector3> _rotateAction;
        
        private readonly IAtomicValue<Vector3> _root;
        private readonly IAtomicValue<Vector3> _targetPoint;
        private readonly IAtomicValue<bool> _isEnabled;

        public LookAtMechanics(
            IAtomicAction<Vector3> rotateAction, 
            IAtomicValue<Vector3> root, 
            IAtomicValue<Vector3> targetPoint,
            IAtomicValue<bool> isEnabled)
        {
            _rotateAction = rotateAction;
            _root = root;
            _targetPoint = targetPoint;
            _isEnabled = isEnabled;
        }

        public void Update()
        {
            if (!_isEnabled.Value)
            {
                return;
            }
            
            var direction = _targetPoint.Value - _root.Value;
            _rotateAction.Invoke(direction);
        }
    }
}