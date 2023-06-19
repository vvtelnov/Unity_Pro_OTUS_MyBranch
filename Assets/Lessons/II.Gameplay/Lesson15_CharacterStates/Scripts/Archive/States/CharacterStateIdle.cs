namespace Lessons.Gameplay.States
{
    public sealed class CharacterStateIdle : IState
    {
        private StateMachine<CharacterStateType> fsm;
        private IAtomicVariable<bool> isDeath;
        private IAtomicVariable<bool> moveRequired;

        public void Construct(
            StateMachine<CharacterStateType> fsm,
            IAtomicVariable<bool> isDeath,
            IAtomicVariable<bool> moveRequired
        )
        {
            this.fsm = fsm;
            this.isDeath = isDeath;
            this.moveRequired = moveRequired;
        }

        void IState.Enter()
        {
            this.isDeath.OnChanged += this.OnDeath;
            this.moveRequired.OnChanged += this.OnMove;
        }

        void IState.Exit()
        {
            this.isDeath.OnChanged -= this.OnDeath;
            this.moveRequired.OnChanged -= this.OnMove;
        }

        private void OnMove(bool required)
        {
            if (required)
            {
                this.fsm.SwitchState(CharacterStateType.MOVE);
            }
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