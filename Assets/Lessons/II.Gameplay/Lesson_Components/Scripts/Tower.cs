using System;
using Atomic.Elements;
using Atomic.Objects;
using Lessons.Lesson_AtomicIntroduсtion;
using Lessons.Lesson_Components.Components;
using Lessons.Lesson_SectionAndVisuals;
using UnityEngine;

namespace Lessons.Lesson_Components.Scripts
{
    //Facade
    public class Tower : AtomicEntity
    {
        [SerializeField] private RotationComponent _rotationComponent;
        [SerializeField] private LifeComponent _lifeComponent;
        [SerializeField] private ShootComponent _shootComponent;

        [SerializeField] private AtomicVariable<GameObject> _target;
        [SerializeField] private AtomicVariable<float> _radius;
        [SerializeField] private AtomicValue<LayerMask> _layerMask;

        private LookAtTargetMechanics _lookAtTargetMechanics;
        private ShootTargetMechanics _shootTargetMechanics;
        private TargetDetectionMechanics _targetDetectionMechanics;

        [Get(HealthAPI.TAKE_DAMAGE_EVENT)]
        public AtomicEvent<int> TakeDamageEvent;
        public AtomicVariable<bool> IsDead;
        public AtomicEvent ShootEvent;
        public AtomicEvent FireAction;
        public Transform FirePoint;

        private void Awake()
        {
            _lifeComponent.Compose(TakeDamageEvent, IsDead);
            _rotationComponent.Construct();
            _rotationComponent.AppendCondition(_lifeComponent.IsAlive);
            
            _shootComponent.Construct(ShootEvent, FirePoint, FireAction);
            _shootComponent.AppendCondition(_lifeComponent.IsAlive);

            var targetPosition = new AtomicFunction<Vector3>(() => _target.Value.transform.position);
            var rootPosition = new AtomicFunction<Vector3>(() => _rotationComponent.RotationRoot.position);
            var hasTarget = new AtomicFunction<bool>(() => _target.Value != null);

            _lookAtTargetMechanics =
                new LookAtTargetMechanics(_rotationComponent.RotateAction, targetPosition, 
                    rootPosition, hasTarget);
            _shootTargetMechanics =
                new ShootTargetMechanics(_shootComponent.ShootEvent, targetPosition, rootPosition, _radius, hasTarget);
            _targetDetectionMechanics = new TargetDetectionMechanics(_radius, rootPosition,
                _target, _layerMask);
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

        private void FixedUpdate()
        {
            _targetDetectionMechanics.FixedUpdate();
        }

        private void Update()
        {
            _shootComponent.Update(Time.deltaTime);
            
            _lookAtTargetMechanics.Update();
            _shootTargetMechanics.Update();
        }
    }
}