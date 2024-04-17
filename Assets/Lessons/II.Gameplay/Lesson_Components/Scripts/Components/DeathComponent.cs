using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class DeathComponent : MonoBehaviour
    {
        [SerializeField] private LifeComponent lifeComponent;

        [ShowInInspector, ReadOnly]
        private bool _isDeath;

        public event Action Death;
        
        private void OnEnable()
        {
            lifeComponent.HealthChanged += OnHealthChanged;
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