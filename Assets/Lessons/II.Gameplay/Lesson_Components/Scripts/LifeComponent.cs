using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class LifeComponent : MonoBehaviour
    {
        [SerializeField] private int _hitPoints = 3;
        public bool IsDead;
        
        [Button]
        public void TakeDamage(int damage)
        {
            if (IsDead)
            {
                return;
            }
            
            _hitPoints -= damage;
            Debug.Log($"Take damage = {damage}");
            
            if (_hitPoints <= 0)
            {
                Debug.Log($"{gameObject.name} is dead!");
                IsDead = true;
            }
        }
    }
}