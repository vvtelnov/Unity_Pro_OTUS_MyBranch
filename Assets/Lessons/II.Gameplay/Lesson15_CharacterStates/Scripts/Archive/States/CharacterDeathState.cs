namespace Lessons.Gameplay.States
{
    public sealed class CharacterDeathState : IState
    {
        private StateMachine<CharacterStateType> fsm;
        private IAtomicVariable<bool> moveRequired;
        private IAtomicVariable<bool> isDeath;

        public void Construct(
            StateMachine<CharacterStateType> fsm,
            IAtomicVariable<bool> moveRequired,
            IAtomicVariable<bool> isDeath
        )
        {
            this.fsm = fsm;
            this.moveRequired = moveRequired;
            this.isDeath = isDeath;
        }

        void IState.Enter()
        {
            this.isDeath.OnChanged += this.OnDeath;
        }

        void IState.Exit()
        {
            this.isDeath.OnChanged -= this.OnDeath;
        }

        private void OnDeath(bool wasDeath)
        {
            if (wasDeath)
            {
                return;
            }

            if (this.moveRequired.Value)
            {
                this.fsm.SwitchState(CharacterStateType.MOVE);
            }
            else
            {
                this.fsm.SwitchState(CharacterStateType.IDLE);
            }
        }
    }
}