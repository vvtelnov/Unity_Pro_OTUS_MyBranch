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
        
        private IAtomicVariable<bool> _isDead;
        private IAtomicEvent<int> _takeDamageEvent;

        public void Compose(IAtomicEvent<int> takeDamageEvent, IAtomicVariable<bool> isDead)
        {
            _takeDamageEvent = takeDamageEvent;
            _isDead = isDead;
        }

        public void OnEnable()
        {
            _takeDamageEvent.Subscribe(TakeDamage);
        }

        public void OnDisable()
        {
            _takeDamageEvent.Unsubscribe(TakeDamage);
        }
        
        public bool IsAlive()
        {
            return !_isDead.Value;
        }
        
        [Button]
        private void TakeDamage(int damage)
        {
            if (_isDead.Value)
            {
                return;
            }
            
            _hitPoints -= damage;
            Debug.Log($"Take damage = {damage}");
            
            if (_hitPoints <= 0)
            {
                _isDead.Value = true;
            }
        }
    }
}