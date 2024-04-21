using System;
using System.Collections.Generic;

namespace Lessons.Lesson_Components.Components
{
    public class CompositeCondition
    {
        private readonly HashSet<Func<bool>> _conditions = new();

        public void AddCondition(Func<bool> condition)
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