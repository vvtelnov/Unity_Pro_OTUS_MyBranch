namespace Lessons.Gameplay.States
{
    public interface IDeathComponent
    {
        void Death();
    }
    
    public sealed class DeathComponent : IDeathComponent
    {
        private readonly IAtomicVariable<bool> isDeath;

        public DeathComponent(IAtomicVariable<bool> isDeath)
        {
            this.isDeath = isDeath;
        }

        void IDeathComponent.Death()
        {
            this.isDeath.Value = true;
        }
    }
}