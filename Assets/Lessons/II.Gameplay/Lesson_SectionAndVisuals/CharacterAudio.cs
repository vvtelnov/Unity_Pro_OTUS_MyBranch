using System;
using Lessons.Lesson_Components;
using UnityEngine;

namespace Lessons.Lesson_SectionAndVisuals
{
    [Serializable]
    public class CharacterAudio 
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _damageAudio;

        private CharacterCore _core;
        
        public void Compose(CharacterCore characterCore)
        {
            _core = characterCore;
        }
        
        public void OnEnable()
        {
            _core.LifeComponent.TakeDamageEvent.Subscribe(OnTakeDamage);
        }

        public void OnDisable()
        {
            _core.LifeComponent.TakeDamageEvent.Unsubscribe(OnTakeDamage);
        }

        private void OnTakeDamage(int obj)
        {
            _audioSource.clip = _damageAudio;
            _audioSource.Play();
        }
    }
}