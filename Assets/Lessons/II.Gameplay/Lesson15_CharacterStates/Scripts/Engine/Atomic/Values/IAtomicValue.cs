namespace Lessons.Gameplay.States
{
    public interface IAtomicValue<out T>
    {
        T Value { get; }
    }
}