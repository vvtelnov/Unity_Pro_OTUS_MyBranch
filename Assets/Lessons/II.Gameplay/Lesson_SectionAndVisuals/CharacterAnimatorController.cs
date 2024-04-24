using Atomic.Elements;
using UnityEngine;

namespace Lessons.Lesson_SectionAndVisuals
{
    public class CharacterAnimatorController
    {
        private static readonly int MainState = Animator.StringToHash("MainState");
        private static readonly int ShootTrigger = Animator.StringToHash("Shoot");

        private const int IDLE = 0;
        private const int MOVE = 1;
        private const int DEATH = 2;
        
        private readonly IAtomicValue<Vector3> _moveDirection;
        private readonly IAtomicValue<bool> _isDead;
        private readonly Animator _animator;
        private readonly AtomicEvent _fireRequest;

        public CharacterAnimatorController(
            IAtomicValue<Vector3> moveDirection,
            IAtomicValue<bool> isDead,
            Animator animator,
            AtomicEvent fireRequest
        )
        {
            _moveDirection = moveDirection;
            _isDead = isDead;
            _animator = animator;
            _fireRequest = fireRequest;
        }

        public void OnEnable()
        {
            _fireRequest.Subscribe(OnFireRequested);
        }

        public void OnDisable()
        {
            _fireRequest.Unsubscribe(OnFireRequested);
        }

        private void OnFireRequested()
        {
            _animator.SetTrigger(ShootTrigger);
        }

        public void Update()
        {
            _animator.SetInteger(MainState, GetAnimationValue());
        }

        private int GetAnimationValue()
        {
            if (_isDead.Value)
            {
                return DEATH;
            }

            if (_moveDirection.Value != Vector3.zero)
            {
                return MOVE;
            }

            return IDLE;
        }
    }
}