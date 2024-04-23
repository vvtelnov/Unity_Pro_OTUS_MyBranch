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
        private AnimatorController _animatorController;
        
        private void Awake()
        {
            _animatorController = new AnimatorController(_character.MoveDirection, _character.IsDead,
                _animator, _character.FireRequest);
        }
        
        private void OnEnable()
        {
            _animatorController.OnEnable();
        }
        
        private void OnDisable()
        {
            _animatorController.OnDisable();
        }
        
        public void Update()
        {
            _animatorController.Update();
        }
    }
}