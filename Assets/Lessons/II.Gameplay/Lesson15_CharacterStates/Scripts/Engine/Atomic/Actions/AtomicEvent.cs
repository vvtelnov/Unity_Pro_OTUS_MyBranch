using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Lessons.Gameplay.States
{
    public sealed class AtomicEvent : IAtomicAction
    {
        private readonly List<IAtomicAction> actions;

        public AtomicEvent()
        {
            this.actions = new List<IAtomicAction>(1);
        }

        public static AtomicEvent operator +(AtomicEvent composite, IAtomicAction action)
        {
            if (composite == null)
            {
                composite = new AtomicEvent();
            }

            composite.actions.Add(action);
            return composite;
        }

        public static AtomicEvent operator -(AtomicEvent composite, IAtomicAction action)
        {
            if (composite == null)
            {
                return null;
            }

            composite.actions.Remove(action);
            return composite;
        }

        public static AtomicEvent operator +(AtomicEvent composite, System.Action action)
        {
            composite += new AtomicAction(action);
            return composite;
        }
        
        [Button]
        public void Invoke()
        {
            foreach (var action in this.actions)
            {
                action.Invoke();
            }
        }
    }

    public class AtomicEvent<T> : IAtomicAction<T>
    {
        protected List<IAtomicAction<T>> actions;

        public AtomicEvent()
        {
            this.actions = new List<IAtomicAction<T>>(1);
        }

        public AtomicEvent(params IAtomicAction<T>[] actions)
        {
            this.actions = new List<IAtomicAction<T>>(actions);
        }

        public static AtomicEvent<T> operator +(AtomicEvent<T> composite, IAtomicAction<T> action)
        {
            if (composite == null)
            {
                composite = new AtomicEvent<T>();
            }

            composite.actions.Add(action);
            return composite;
        }
        
        public static AtomicEvent<T> operator +(AtomicEvent<T> composite, System.Action<T> action)
        {
            composite += new AtomicAction<T>(action);
            return composite;
        }
        
        public static AtomicEvent<T> operator -(AtomicEvent<T> composite, IAtomicAction<T> action)
        {
            if (composite == null)
            {
                return null;
            }

            composite.actions.Remove(action);
            return composite;
        }

        [Button]
        public void Invoke(T args)
        {
            foreach (var listener in this.actions)
            {
                listener.Invoke(args);
            }
        }
    }
}