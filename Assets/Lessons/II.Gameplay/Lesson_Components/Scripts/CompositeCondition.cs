using System;
using System.Collections.Generic;

namespace Lessons.Lesson_Components
{
    public sealed class CompositeCondition
    {
        private readonly List<Func<bool>> _conditions = new();

        public void Add(Func<bool> condition)
        {
            _conditions.Add(condition);
        }

        public bool IsTrue()
        {
            foreach (var condition in _conditions)
            {
                if (!condition.Invoke())
                {
                    return false;
                }
            }

            return true;
        }
    }
}