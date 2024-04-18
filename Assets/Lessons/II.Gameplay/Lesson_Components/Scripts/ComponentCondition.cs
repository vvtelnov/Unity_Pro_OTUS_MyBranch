using System;
using System.Collections.Generic;

namespace Lessons.Lesson_Components
{
    public sealed class ComponentCondition
    {
        private readonly List<Func<bool>> _conditions = new();

        public void Add(Func<bool> condition)
        {
            _conditions.Add(condition);
        }

        public void Remove(Func<bool> condition)
        {
            _conditions.Remove(condition);
        }

        public bool IsTrue()
        {
            for (int i = 0; i < _conditions.Count; i++)
            {
                if (!_conditions[i].Invoke())
                {
                    return false;
                }
            }

            return true;
        }
    }
}