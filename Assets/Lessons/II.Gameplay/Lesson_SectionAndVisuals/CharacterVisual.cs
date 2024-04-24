using System;
using Lessons.Lesson_Components;
using UnityEngine;

namespace Lessons.Lesson_SectionAndVisuals
{
    [Serializable]
    public class CharacterAnimation
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimatorDispatcher _animatorDispatcher;
        private CharacterCore _core;

        public void Compose(CharacterCore core)
        {
            _core = core;
        }
      
        public void OnEnable()
        {
            _core.MoveComponent.MoveDirection.Subscribe(OnMoveChanged);
            _core.ShootComponent.ShootRequest.Subscribe(OnShootRequest);
            _animatorDispatcher.SubscribeOnEvent("shoot", _core.ShootComponent.ShootAction.Invoke);
        }
        
        public void OnDisable()
        {
            _animatorDispatcher.UnsubscribeOnEvent("shoot", _core.ShootComponent.ShootAction.Invoke);
            _core.MoveComponent.MoveDirection.Unsubscribe(OnMoveChanged);
            _core.ShootComponent.ShootRequest.Unsubscribe(OnShootRequest);
        }
        
        private void OnShootRequest()
        {
            if (_core.ShootComponent.CanFire())
            {
                _animator.SetTrigger("Shoot");
            }
        }

        private void OnMoveChanged(Vector3 direction)
        {
            _animator.SetBool("IsMoving", direction != Vector3.zero);
        }
    }
}