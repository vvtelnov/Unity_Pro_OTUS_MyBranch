using UnityEngine;

namespace Lessons.Gameplay.States
{
    public interface IMoveComponent
    {
        void Move(Vector3 direction);
    }
    
    public sealed class MoveComponent : IMoveComponent
    {
        private readonly IAtomicAction<Vector3> onMove;

        public MoveComponent(IAtomicAction<Vector3> onMove)
        {
            this.onMove = onMove;
        }

        void IMoveComponent.Move(Vector3 direction)
        {
            this.onMove.Invoke(direction);
        }
    }
}