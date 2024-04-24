using System;
using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components.Components
{
    [Serializable]
    public class LifeComponent 
    {
        [SerializeField] private int _hitPoints;
        
        public AtomicEvent<int> TakeDamageEvent;
        public AtomicVariable<bool> IsDead;

        public void OnEnable()
        {
            TakeDamageEvent.Subscribe(TakeDamage);
        }

        public void OnDisable()
        {
            TakeDamageEvent.Unsubscribe(TakeDamage);
        }
        
        public bool IsAlive()
        {
            return !IsDead.Value;
        }
        
        [Button]
        private void TakeDamage(int damage)
        {
            if (IsDead.Value)
            {
                return;
            }
            
            _hitPoints -= damage;
            Debug.Log($"Take damage = {damage}");
            
            if (_hitPoints <= 0)
            {
                IsDead.Value = true;
            }
        }
    }
}