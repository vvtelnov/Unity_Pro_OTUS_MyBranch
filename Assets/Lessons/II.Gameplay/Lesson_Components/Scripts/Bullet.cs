using System;
using Lessons.Lesson_Components.Components;
using UnityEngine;

namespace Lessons.Lesson_Components.Scripts
{
    public class Bullet : MonoBehaviour
    {
        [field: SerializeField] public MoveComponent MoveComponent { get; private set; }
        
        [SerializeField] private int _damage = 1;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
            }
        }

        private void Update()
        {
            MoveComponent.Update(Time.deltaTime);
        }
    }
}