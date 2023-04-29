using System;
using AI.Commands;

namespace Game.GameEngine.AI
{
    public sealed class Component_EnqueueCommand : IComponent_EnqueueCommand
    {
        public bool IsRunning
        {
            get { return this.enqueuer.IsRunning; }
        }

        private readonly ICommandEnqueuer<Type> enqueuer;

        public Component_EnqueueCommand(ICommandEnqueuer<Type> enqueuer)
        {
            this.enqueuer = enqueuer;
        }

        public void Enqueue<T>(T args)
        {
            this.enqueuer.Enqueue(args);
        }

        public void Interrupt()
        {
            this.enqueuer.Interrupt();
        }
    }
}