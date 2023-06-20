// using System;
// using Lessons.Character;
// using Lessons.Character.Engines;
// using Lessons.Character.Variables;
// using UnityEngine;
//
// namespace Lessons.StateMachines.States
// {
//     [Serializable]
//     public sealed class MoveState : IState
//     {
//         [SerializeField]
//         private Animator animator;
//
//         private MovementDirectionVariable _movementDirection;
//         private MovementEngine _movementEngine;
//         private RotationEngine _rotationEngine;
//
//         public void Construct(MovementDirectionVariable movementDirection, MovementEngine movementEngine,
//             RotationEngine rotationEngine)
//         {
//             _movementDirection = movementDirection;
//             _movementEngine = movementEngine;
//             _rotationEngine = rotationEngine;
//         }
//
//         void IState.Enter()
//         {
//             animator.SetInteger("State", (int) AnimatorState.Move);
//             
//             _movementDirection.OnChanged += UpdateDirection;
//             UpdateDirection(_movementDirection);
//         }
//
//         void IState.Exit()
//         {
//             _movementDirection.OnChanged -= UpdateDirection;
//         }
//
//         private void UpdateDirection(Vector3 direction)
//         {
//             _movementEngine.SetDirection(direction);
//             _rotationEngine.SetDirection(direction);
//         }
//     }
// }