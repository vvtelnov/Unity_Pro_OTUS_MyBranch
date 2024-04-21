using System;
using Atomic.Elements;
using Atomic.Extensions;
using Lessons.Lesson_AtomicIntroduсtion.Mechanics;
using Lessons.Lesson_Components.Components;
using UnityEngine;

namespace Lessons.Lesson_Components.Scripts
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private RotationComponent _rotationComponent;
        [SerializeField] private LifeComponent _lifeComponent;
        [SerializeField] private ShootComponent _shootComponent;

        [SerializeField] private AtomicVariable<GameObject> _targetPoint;
        [SerializeField] private AtomicVariable<float> _radius;
        [SerializeField] private AtomicValue<LayerMask> _layerMask;

        private LookTargetMechanics _lookTargetMechanics;
        private ShootTowardsTargetMechanics _shootMechanics;
        private TargetDetectionMechanics _targetDetectionMechanics;

        private void Awake()
        {
            _rotationComponent.Let(it =>
            {
                it.Compose();
                it.AppendCondition(_lifeComponent.IsAlive);
            });

            _shootComponent.Let(it =>
            {
                it.Compose();
                it.AppendCondition(_lifeComponent.IsAlive);
                //Еще что-то настроить
            });

            var targetPosition = this.AsFunction(it =>
            {
                return it._targetPoint.Value.transform.position;
            });

            var hasTarget = new AtomicFunction<bool>(() =>
            {
                return _targetPoint.Value != null;
            });
            
            _lookTargetMechanics = 
                new LookTargetMechanics(_rotationComponent.RotationRoot, targetPosition,
                    _rotationComponent, hasTarget);
            
            _shootMechanics = 
                new ShootTowardsTargetMechanics(_rotationComponent.Position, targetPosition,
                new AtomicAction(()=> Debug.Log("Pam pam")), _radius, hasTarget);

            _targetDetectionMechanics =
                new TargetDetectionMechanics(_radius, _rotationComponent.Position, _targetPoint, _layerMask);
        }

        private void FixedUpdate()
        {
            _targetDetectionMechanics.FixedUpdate();
        }

        private void Update()
        {
            _shootComponent.Update(Time.deltaTime);
            
            _lookTargetMechanics.Update();
            _shootMechanics.Update();
        }
    }
}