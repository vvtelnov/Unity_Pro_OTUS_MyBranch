using System;
using Lessons.Lesson_Components;
using UnityEngine;

namespace Lessons.Lesson_SectionAndVisuals
{
    [Serializable]
    public class CharacterVFX
    {
        public ParticleSystem ShootVfx;
        public ParticleSystem DamageVfx;
        
        private CharacterCore _core;

        public void Compose(CharacterCore core)
        {
            _core = core;
        }
        
        public void OnEnable()
        {
            _core.ShootComponent.ShootEvent.Subscribe(OnShoot);
            _core.LifeComponent.TakeDamageEvent.Subscribe(OnTakeDamage);
        }

        public void OnDisable()
        {
            _core.ShootComponent.ShootEvent.Unsubscribe(OnShoot);
            _core.LifeComponent.TakeDamageEvent.Unsubscribe(OnTakeDamage);
        }

        private void OnTakeDamage(int value)
        {
            //Может быть обработка в зависимости от урона
            DamageVfx.Play();
        }

        private void OnShoot()
        {
            ShootVfx.transform.position = _core.ShootComponent.FirePoint.position;
            ShootVfx.Play();
        }
    }
}