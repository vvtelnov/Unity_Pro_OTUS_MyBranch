namespace Lessons.Gameplay.States
{
    public interface IAtomicVariable<T> : IAtomicValue<T>
    {
        AtomicEvent<T> OnChanged { get; set; }
        
        new T Value { get; set; }
    }
}