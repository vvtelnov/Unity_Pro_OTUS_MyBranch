using Lessons.Lesson_Components;
using UnityEngine;

namespace Lessons.Lesson_SectionAndVisuals
{
    public class CharacterVisual : MonoBehaviour
    {
        //Data
        [SerializeField] private Character _character;
        [SerializeField] private Animator _animator;
        
        //Logic
        private CharacterAnimatorController _characterAnimatorController;
        
        private void Awake()
        {
            _characterAnimatorController = new CharacterAnimatorController(_character.MoveDirection, _character.IsDead,
                _animator, _character.FireRequest);
        }
        
        private void OnEnable()
        {
            _characterAnimatorController.OnEnable();
        }
        
        private void OnDisable()
        {
            _characterAnimatorController.OnDisable();
        }
        
        public void Update()
        {
            _characterAnimatorController.Update();
        }
    }
}