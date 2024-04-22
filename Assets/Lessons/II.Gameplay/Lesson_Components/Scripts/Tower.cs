using Atomic.Elements;
using Lessons.Lesson_AtomicIntroduсtion.Scripts;
using Lessons.Lesson_Components.Components;
using UnityEngine;

namespace Lessons.Lesson_Components.Scripts
{
    public class Tower : MonoBehaviour, IDamageable
    {
        [SerializeField] private RotationComponent _rotationComponent;
        [SerializeField] private LifeComponent _lifeComponent;
        [SerializeField] private ShootComponent _shootComponent;

        [SerializeField] private Transform _targetPoint;
        [SerializeField] private AtomicVariable<float> _radius;

        private LookAtMechanics _lookAtMechanics;
        private ShootTargetsMechanics _shootTargetsMechanics;

        private void Awake()
        {
            _rotationComponent.Construct();
            _rotationComponent.AppendCondition(_lifeComponent.IsAlive);
            
            _shootComponent.Construct();
            _shootComponent.AppendCondition(_lifeComponent.IsAlive);

            var rotationPosition = new AtomicFunction<Vector3>(()=>_rotationComponent.RotationRoot.position);
            var targetPosition = new AtomicFunction<Vector3>(()=>_targetPoint.position);
            
            _lookAtMechanics = new LookAtMechanics(_rotationComponent.RotateAction, rotationPosition, targetPosition);
            _shootTargetsMechanics 
                = new ShootTargetsMechanics(_shootComponent.ShootAction, rotationPosition, targetPosition, _radius);
        }

        private void Update()
        {
            _shootComponent.Update(Time.deltaTime);
            
            _lookAtMechanics.Update();
            _shootTargetsMechanics.Update();
        }

        public void TakeDamage(int damage)
        {
            _lifeComponent.TakeDamage(damage);
        }
    }
}