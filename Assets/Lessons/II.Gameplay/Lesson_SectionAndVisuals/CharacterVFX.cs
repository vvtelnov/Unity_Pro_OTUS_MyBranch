using Lessons.Lesson_Components;
using UnityEngine;

namespace Lessons.Lesson_SectionAndVisuals
{
    public class CharacterVFX : MonoBehaviour
    {
        [SerializeField] private Character _character;

        public ParticleSystem ShootVfx;
        public ParticleSystem DamageVfx;

        private void OnEnable()
        {
            _character.ShootEvent.Subscribe(OnFire);
            _character.TakeDamageEvent.Subscribe(OnTakeDamage);
        }

        private void OnDisable()
        {
            _character.ShootEvent.Unsubscribe(OnFire);
            _character.TakeDamageEvent.Unsubscribe(OnTakeDamage);
        }

        private void OnTakeDamage(int value)
        {
            //Может быть обработка в зависимости от урона
            DamageVfx.Play();
        }

        private void OnFire()
        {
            ShootVfx.transform.position = _character.FirePoint.position;
            ShootVfx.Play();
        }
    }
}