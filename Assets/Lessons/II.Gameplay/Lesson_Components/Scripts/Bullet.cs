using System;
using Lessons.Lesson_Components.Components;
using UnityEngine;

namespace Lessons.Lesson_Components.Scripts
{
    //Facade
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _damage = 1;
        [field: SerializeField] public MoveComponent MoveComponent { get; private set; }
        
        

        private void Update()
        {
            MoveComponent.Update(Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
            }
        }
    }
}