using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components.Components
{
    [Serializable]
    public class LifeComponent 
    {
        [SerializeField] private int _hitPoints;
        [SerializeField] private bool _isDead;
        
        public bool IsAlive()
        {
            return !_isDead;
        }
        
        [Button]
        public void TakeDamage(int damage)
        {
            if (_isDead)
            {
                return;
            }
            
            _hitPoints -= damage;
            Debug.Log($"Take damage = {damage}");
            
            if (_hitPoints <= 0)
            {
                _isDead = true;
            }
        }
    }
}