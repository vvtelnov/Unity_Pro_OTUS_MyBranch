using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public interface IHitPointsComponent
    {
        event Action<int> HealthChanged;
        void TakeDamage(int damage);
    }
    
    public class HitPointsComponent : MonoBehaviour, IHitPointsComponent
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
            }
        }
    }
}