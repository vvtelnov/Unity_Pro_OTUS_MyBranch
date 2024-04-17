using System;

namespace Lessons.Lesson_Components
{
    public interface ILifeComponent
    {
        event Action<int> HealthChanged;
        void TakeDamage(int damage);
    }
}