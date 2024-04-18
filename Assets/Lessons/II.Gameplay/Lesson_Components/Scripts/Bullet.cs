using System;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private int _damage = 1;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out DealDamageAction character))
            {
                character.DealDamage(_damage);
            }
        }
    }
}
