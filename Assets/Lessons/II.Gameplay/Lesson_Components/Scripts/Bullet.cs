using Lessons.Lesson_Components.Components;
using UnityEngine;

namespace Lessons.Lesson_Components.Scripts
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _damage = 1;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TakeDamageAction damageMechanics))
            {
                damageMechanics.TakeDamage(_damage);
            }
        }
    }
}