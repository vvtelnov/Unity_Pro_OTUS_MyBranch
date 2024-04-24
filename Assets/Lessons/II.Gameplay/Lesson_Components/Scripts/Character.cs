using Atomic.Elements;
using Atomic.Objects;
using Lessons.Lesson_SectionAndVisuals;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    //Facade
    public class Character : AtomicEntity
    {
        #region Interface

        [Get(HealthAPI.TAKE_DAMAGE_ACTION)] 
        public IAtomicAction<int> TakeDamageAction => _core.LifeComponent.TakeDamageEvent;
        
        [Get(ShootAPI.SHOOT_REQUEST)]
        public AtomicEvent ShootRequest => _core.ShootComponent.ShootRequest;
        
        [Get(ShootAPI.SHOOT_ACTION)]
        public AtomicEvent ShootAction => _core.ShootComponent.ShootAction;

        [Get(MoveAPI.MOVE_DIRECTION)] 
        public IAtomicVariable<Vector3> MoveDirection => _core.MoveComponent.MoveDirection;

        #endregion

        #region Core

        [SerializeField]
        private CharacterCore _core;
        
        [SerializeField] 
        private CharacterAnimation _characterAnimation;

        [SerializeField] 
        private CharacterVFX _characterVFX;

        [SerializeField] 
        private CharacterAudio _characterAudio;

        private void Awake()
        {
            _core.Compose();
            _characterAnimation.Compose(_core);
            _characterVFX.Compose(_core);
            _characterAudio.Compose(_core);
        }

        private void OnEnable()
        {
            _core.OnEnable();
            _characterAnimation.OnEnable();
            _characterVFX.OnEnable();
            _characterAudio.OnEnable();
        }

        private void OnDisable()
        {
            _core.OnDisable();
            _characterAnimation.OnDisable();
            _characterVFX.OnDisable();
            _characterAudio.OnDisable();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _core.Update(deltaTime);
        }

        #endregion
    }
}
