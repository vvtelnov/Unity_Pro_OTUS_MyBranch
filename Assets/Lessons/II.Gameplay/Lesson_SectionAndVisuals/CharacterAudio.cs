using Lessons.Lesson_Components;
using UnityEngine;

namespace Lessons.Lesson_SectionAndVisuals
{
    public class CharacterAudio : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private AudioSource _audioSource;
        
        public AudioClip DamageAudio;

        private void OnEnable()
        {
            _character.TakeDamageEvent.Subscribe(OnTakeDamage);
        }

        private void OnDisable()
        {
            _character.TakeDamageEvent.Unsubscribe(OnTakeDamage);
        }

        private void OnTakeDamage(int obj)
        {
            _audioSource.clip = DamageAudio;
            _audioSource.Play();
        }
    }
}