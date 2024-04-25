using System;
using Atomic.Elements;
using Lessons.Lesson_AtomicIntroduсtion;
using Lessons.Lesson_Components.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    //Facade
    public class Character : MonoBehaviour, IDamageable
    {
        //Interfaces
        public MoveComponent MoveComponent => _moveComponent;
        public RotationComponent RotationComponent => _rotationComponent;
        public ShootComponent ShootComponent => _shootComponent;
        
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private LifeComponent _lifeComponent;
        [SerializeField] private RotationComponent _rotationComponent;
        [SerializeField] private ShootComponent _shootComponent;

        [SerializeField] private Transform _targetPoint;

        private LookAtTargetMechanics _lookAtTargetMechanics;
        
        private void Awake()
        {
            _moveComponent.AppendCondition(_lifeComponent.IsAlive);
            _moveComponent.AppendCondition(_shootComponent.CanFire);
            
            _rotationComponent.Construct();
            _rotationComponent.AppendCondition(_lifeComponent.IsAlive);

            var targetPosition = new AtomicFunction<Vector3>(() =>
            {
                return _targetPoint.position;
            });

            var rootPosition = new AtomicFunction<Vector3>(() =>
            {
                return _rotationComponent.RotationRoot.position;
            });

            var hasTarget = new AtomicFunction<bool>(() =>
            {
                return _targetPoint != null;
            });
            
            _lookAtTargetMechanics =
                new LookAtTargetMechanics(_rotationComponent.RotateAction, targetPosition,
                    rootPosition, hasTarget);
        }

        private void Update()
        {
            _moveComponent.Update(Time.deltaTime);
            _shootComponent.Update(Time.deltaTime);
            
           _lookAtTargetMechanics.Update();
        }

        public void TakeDamage(int damage)
        {
            _lifeComponent.TakeDamage(damage);
        }
    }
}
