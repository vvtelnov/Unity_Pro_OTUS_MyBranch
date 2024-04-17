using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class LifeComponent : MonoBehaviour, ILifeComponent
    {
        [SerializeField] private int _hitPoints = 3;
        
        [ShowInInspector, ReadOnly]
        private bool _isDeath;

        public event Action Death;
        public event Action<int> HealthChanged;

        [Button]
        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;
            Debug.Log($"Take damage {damage}");
            HealthChanged?.Invoke(_hitPoints);
            OnHealthChanged(_hitPoints);
        }
        
        private void OnHealthChanged(int hitPoints)
        {
            if (_isDeath)
            {
                return;
            }
            
            if (hitPoints <= 0)
            {
                _isDeath = true;
                Death?.Invoke();
                Debug.Log("Death");
            }
        }
    }
}