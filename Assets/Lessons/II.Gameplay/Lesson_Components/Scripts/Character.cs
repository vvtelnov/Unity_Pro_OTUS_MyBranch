using System;
using Atomic.Elements;
using Atomic.Objects;
using Lessons.Lesson_AtomicIntroduсtion;
using Lessons.Lesson_Components.Components;
using Lessons.Lesson_SectionAndVisuals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    //Facade
    public class Character : AtomicEntity
    {
        [Get(HealthAPI.TAKE_DAMAGE_EVENT)]
        public AtomicEvent<int> TakeDamageEvent;
        
        public Transform FirePoint;
        
        public AtomicVariable<bool> IsDead;
        
        [Get(FireAPI.FIRE_REQUEST)]
        public AtomicEvent FireRequest;
        
        [Get(FireAPI.REQUESTED_FIRE_EVENT)]
        public AtomicEvent RequestedFireEvent;

        public AtomicEvent FireEvent;
        
        [Get(MoveAPI.MOVE_ACTION)]
        public AtomicVariable<Vector3> MoveDirection;
        
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private LifeComponent _lifeComponent;
        [SerializeField] private RotationComponent _rotationComponent;
        [SerializeField] private ShootComponent _shootComponent;

        [SerializeField] private Transform _targetPoint;
        [SerializeField] private Collider _collider;

        private LookAtTargetMechanics _lookAtTargetMechanics;

        private void Awake()
        {
            _lifeComponent.Compose(TakeDamageEvent, IsDead);
            
            _moveComponent.Construct(MoveDirection);
            _moveComponent.AppendCondition(_lifeComponent.IsAlive);
            _moveComponent.AppendCondition(_shootComponent.CanFire);
            
            _rotationComponent.Construct();
            _rotationComponent.AppendCondition(_lifeComponent.IsAlive);
            
            _shootComponent.Construct(RequestedFireEvent, FirePoint, FireEvent);

            var targetPosition = new AtomicFunction<Vector3>(() => _targetPoint.position);
            var rootPosition = new AtomicFunction<Vector3>(() => _rotationComponent.RotationRoot.position);
            var hasTarget = new AtomicFunction<bool>(() => _targetPoint != null);
            
            _lookAtTargetMechanics =
                new LookAtTargetMechanics(_rotationComponent.RotateAction, targetPosition,
                    rootPosition, hasTarget);
            
            //Куда эту логику?
            IsDead.Subscribe(isDead => _collider.enabled = !isDead);
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
