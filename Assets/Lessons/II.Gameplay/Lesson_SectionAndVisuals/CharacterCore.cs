using System;
using Atomic.Elements;
using Atomic.Objects;
using Lessons.Lesson_AtomicIntroduсtion;
using Lessons.Lesson_Components.Components;
using UnityEngine;

namespace Lessons.Lesson_SectionAndVisuals
{
    [Serializable]
    public class CharacterCore
    {
        public MoveComponent MoveComponent;
        public LifeComponent LifeComponent;
        public RotationComponent RotationComponent;
        public ShootComponent ShootComponent;

        public Transform TargetPoint;
        public Collider Collider;

        private LookAtTargetMechanics _lookAtTargetMechanics;

        public void Compose()
        {
            MoveComponent.AppendCondition(LifeComponent.IsAlive);
            
            RotationComponent.Construct();
            RotationComponent.AppendCondition(LifeComponent.IsAlive);
            
            var targetPosition = new AtomicFunction<Vector3>(() => TargetPoint.position);
            var rootPosition = new AtomicFunction<Vector3>(() => RotationComponent.RotationRoot.position);
            var hasTarget = new AtomicFunction<bool>(() => TargetPoint != null);
            
            _lookAtTargetMechanics =
                new LookAtTargetMechanics(RotationComponent.RotateAction, targetPosition,
                    rootPosition, hasTarget);
            
            //Куда эту логику?
            LifeComponent.IsDead.Subscribe(isDead => Collider.enabled = !isDead);
        }

        public void OnEnable()
        {
            LifeComponent.OnEnable();
            ShootComponent.OnEnable();
        }

        public void OnDisable()
        {
            LifeComponent.OnDisable();
            ShootComponent.OnDisable();
        }

        public void Update(float deltaTime)
        {
            MoveComponent.Update(deltaTime);
            ShootComponent.Update(deltaTime);
            
            _lookAtTargetMechanics.Update();
        }
    }
}