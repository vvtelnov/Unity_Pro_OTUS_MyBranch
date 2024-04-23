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
        [SerializeField] private bool _isDead;

        private IAtomicEvent<int> _takeDamageEvent;

        public void Compose(IAtomicEvent<int> takeDamageEvent)
        {
            _takeDamageEvent = takeDamageEvent;
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
            return !_isDead;
        }
        
        [Button]
        private void TakeDamage(int damage)
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