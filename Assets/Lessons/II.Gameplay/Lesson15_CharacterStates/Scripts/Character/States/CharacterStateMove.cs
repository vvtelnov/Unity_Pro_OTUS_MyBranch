using UnityEngine;

namespace Lessons.Gameplay.States
{
    public sealed class CharacterStateMove : FixedState
    {
        private bool enabled;

        private Transform moveTransform;
        private IAtomicVariable<bool> moveRequired;
        private IAtomicValue<Vector3> moveDirection;
        private IAtomicValue<float> speed;

        private StateMachine<CharacterStateType> fsm;
        private IAtomicVariable<bool> isDeath;

        public void Construct(
            IAtomicVariable<bool> moveRequired,
            IAtomicValue<Vector3> moveDirection,
            IAtomicValue<float> speed,
            Transform moveTransform
        )
        {
            this.moveTransform = moveTransform;
            this.moveRequired = moveRequired;
            this.moveDirection = moveDirection;
            this.speed = speed;
        }

        public void Construct(StateMachine<CharacterStateType> fsm, IAtomicVariable<bool> isDeath)
        {
            this.fsm = fsm;
            this.isDeath = isDeath;
        }

        protected override void FixedUpdate(float deltaTime)
        {
            if (this.moveRequired.Value)
            {
                this.moveTransform.position += this.moveDirection.Value * (this.speed.Value * deltaTime);
                this.moveTransform.rotation = Quaternion.LookRotation(this.moveDirection.Value, Vector3.up);
                this.moveRequired.Value = false;
            }
            else
            {
                this.fsm.SwitchState(CharacterStateType.IDLE);
            }
        }

        public override void Enter()
        {
            base.Enter();
            this.isDeath.OnChanged += this.OnDeath;
        }

        public override void Exit()
        {
            base.Exit();
            this.isDeath.OnChanged -= this.OnDeath;
        }

        private void OnDeath(bool wasDeath)
        {
            if (wasDeath)
            {
                this.fsm.SwitchState(CharacterStateType.DEATH);
            }
        }
    }
}