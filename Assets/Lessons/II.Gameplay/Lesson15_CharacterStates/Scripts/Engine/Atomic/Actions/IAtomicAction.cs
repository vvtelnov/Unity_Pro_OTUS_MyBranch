namespace Lessons.Gameplay.States
{
    public interface IAtomicAction
    {
        void Invoke();
    }

    public interface IAtomicAction<in T>
    {
        void Invoke(T args);
    }
}