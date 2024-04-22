using System;
using Lessons.Lesson_Components.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private LifeComponent _lifeComponent;
        [SerializeField] private RotationComponent _rotationComponent;
        [SerializeField] private ShootComponent _shootComponent;
        
        private void Awake()
        {
            _moveComponent.AppendCondition(_lifeComponent.IsAlive);
            _moveComponent.AppendCondition(_shootComponent.CanFire);
            
            _rotationComponent.AppendCondition(_lifeComponent.IsAlive);
        }
    }
}
