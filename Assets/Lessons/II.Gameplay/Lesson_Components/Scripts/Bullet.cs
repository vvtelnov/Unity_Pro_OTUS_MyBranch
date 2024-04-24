using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Lessons.Lesson_Components.Components;
using Lessons.Lesson_SectionAndVisuals;
using UnityEngine;

namespace Lessons.Lesson_Components.Scripts
{
    //Facade
    public class Bullet : AtomicEntity
    {
        [SerializeField] private int _damage = 1;
        [field: SerializeField] public MoveComponent MoveComponent { get; private set; }

        private void Update()
        {
            MoveComponent.Update(Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IAtomicEntity character))
            {
                character.InvokeAction(HealthAPI.TAKE_DAMAGE_ACTION, _damage);
            }
        }
    }
}