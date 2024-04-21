using System;
using Atomic.Elements;
using Atomic.Extensions;
using Lessons.Lesson_AtomicIntroduсtion.Mechanics;
using Lessons.Lesson_Components.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    //Facade
    public class Character : MonoBehaviour, IDamageable
    {
        //Interface
        public MoveComponent MoveComponent => _moveComponent;
        public ShootComponent ShootComponent => _shootComponent;

        //Core
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private LifeComponent _lifeComponent;
        [SerializeField] private RotationComponent _rotationComponent;
        [SerializeField] private ShootComponent _shootComponent;

        [SerializeField] private Transform _targetPoint;

        private LookTargetMechanics _lookTargetMechanics;
        
        private void Awake()
        {
            _moveComponent.AppendCondition(_lifeComponent.IsAlive);
            _moveComponent.AppendCondition(_shootComponent.CanFire);
            
            _rotationComponent.AppendCondition(_lifeComponent.IsAlive);
            
            var targetPosition = this.AsFunction(it =>
            {
                return it._targetPoint.position;
            });
            
            _lookTargetMechanics = new LookTargetMechanics(_rotationComponent.RotationRoot,
                targetPosition, _rotationComponent, new AtomicValue<bool>(true));
        }

        private void Update()
        {
            _moveComponent.Update(Time.deltaTime);
            _shootComponent.Update(Time.deltaTime);
            
            _lookTargetMechanics.Update();
        }

        public void TakeDamage(int damage)
        {
            _lifeComponent.TakeDamage(damage);
        }
    }
}
