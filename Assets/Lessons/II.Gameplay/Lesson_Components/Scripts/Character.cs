using Atomic.Elements;
using Lessons.Lesson_AtomicIntroduсtion.Scripts;
using Lessons.Lesson_Components.Components;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    //Facade
    public class Character : MonoBehaviour, IDamageable
    {
        //Interfaces
        [field: SerializeField] public MoveComponent MoveComponent { get; private set; }
        [field: SerializeField] public RotationComponent RotationComponent { get; private set; }
        [field: SerializeField] public ShootComponent ShootComponent { get; private set; }

        [SerializeField] private LifeComponent _lifeComponent;
        [SerializeField] private Transform _targetPoint;
        
        //Logic
        private LookAtMechanics _lookAtMechanics;
        
        private void Awake()
        {
            MoveComponent.AppendCondition(_lifeComponent.IsAlive);
            MoveComponent.AppendCondition(ShootComponent.CanFire);
            
            RotationComponent.Construct();
            RotationComponent.AppendCondition(_lifeComponent.IsAlive);
            
            ShootComponent.Construct();

            var rotationPoint = new AtomicFunction<Vector3>(()=>RotationComponent.RotationRoot.position);
            var targetPoint = new AtomicFunction<Vector3>(()=>_targetPoint.position);
            
            _lookAtMechanics = 
                new LookAtMechanics(RotationComponent.RotateAction, rotationPoint, targetPoint);
        }

        private void Update()
        {
            MoveComponent.Update(Time.deltaTime);
            ShootComponent.Update(Time.deltaTime);
            
            _lookAtMechanics.Update();
        }

        public void TakeDamage(int damage)
        {
            _lifeComponent.TakeDamage(damage);
        }
    }
}
