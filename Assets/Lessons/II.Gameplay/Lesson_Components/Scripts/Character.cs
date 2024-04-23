using System;
using Atomic.Elements;
using Lessons.Lesson_AtomicIntroduсtion;
using Lessons.Lesson_Components.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    //Facade
    public class Character : MonoBehaviour
    {
        //Interfaces
        public MoveComponent MoveComponent => _moveComponent;
        public RotationComponent RotationComponent => _rotationComponent;
        public ShootComponent ShootComponent => _shootComponent;

        public AtomicEvent<int> TakeDamageEvent;
        public AtomicEvent ShootEvent;
        public Transform FirePoint;
        
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private LifeComponent _lifeComponent;
        [SerializeField] private RotationComponent _rotationComponent;
        [SerializeField] private ShootComponent _shootComponent;

        [SerializeField] private Transform _targetPoint;

        private LookAtTargetMechanics _lookAtTargetMechanics;
        
        private void Awake()
        {
            _lifeComponent.Compose(TakeDamageEvent);
            
            _moveComponent.AppendCondition(_lifeComponent.IsAlive);
            _moveComponent.AppendCondition(_shootComponent.CanFire);
            
            _rotationComponent.Construct();
            _rotationComponent.AppendCondition(_lifeComponent.IsAlive);
            
            _shootComponent.Construct(ShootEvent, FirePoint);

            var targetPosition = new AtomicFunction<Vector3>(() => _targetPoint.position);
            var rootPosition = new AtomicFunction<Vector3>(() => _rotationComponent.RotationRoot.position);
            var hasTarget = new AtomicFunction<bool>(() => _targetPoint != null);
            
            _lookAtTargetMechanics =
                new LookAtTargetMechanics(_rotationComponent.RotateAction, targetPosition,
                    rootPosition, hasTarget);
        }

        private void OnEnable()
        {
            _lifeComponent.OnEnable();
            _shootComponent.OnEnable();
        }

        private void OnDisable()
        {
            _lifeComponent.OnDisable();
            _shootComponent.OnDisable();
        }

        private void Update()
        {
            _moveComponent.Update(Time.deltaTime);
            _shootComponent.Update(Time.deltaTime);
            
           _lookAtTargetMechanics.Update();
        }
    }
}
