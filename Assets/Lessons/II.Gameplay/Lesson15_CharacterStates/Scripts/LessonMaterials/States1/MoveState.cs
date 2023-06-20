// using System;
// using Lessons.Character;
// using Lessons.Character.Engines;
// using Lessons.Character.Variables;
// using Lessons.Engine.Atomic.Values;
// using UnityEngine;
//
// namespace Lessons.States
// {
//     [Serializable]
//     public sealed class MoveState : IState
//     {
//         [SerializeField]
//         private Animator animator;
//         
//         [SerializeField]
//         private float movementSpeed = 6f;
//
//         [SerializeField]
//         private float rotationSpeed = 10f;
//
//         private MovementDirectionVariable _movementDirection;
//         private MovementEngine _movementEngine;
//         private RotationEngine _rotationEngine;
//
//         private StateMachine _stateMachine;
//         private AtomicVariable<bool> _isAlive;
//
//         public void Construct(MovementDirectionVariable movementDirection, MovementEngine movementEngine,
//             RotationEngine rotationEngine)
//         {
//             _movementDirection = movementDirection;
//             _movementEngine = movementEngine;
//             _rotationEngine = rotationEngine;
//         }
//
//         public void Construct(StateMachine stateMachine, AtomicVariable<bool> isAlive)
//         {
//             _stateMachine = stateMachine;
//             _isAlive = isAlive;
//         }
//
//         void IState.Enter()
//         {
//             animator.SetInteger("State", (int) AnimatorState.Move);
//             
//             _movementDirection.OnChanged += UpdateDirection;
//
//             _isAlive.OnChanged += OnIsAliveChanged;
//             _movementDirection.MovementFinished += OnMovementFinished;
//
//             UpdateDirection(_movementDirection);
//         }
//
//         void IState.Exit()
//         {
//             _movementDirection.OnChanged -= UpdateDirection;
//             
//             _isAlive.OnChanged -= OnIsAliveChanged;
//             _movementDirection.MovementFinished -= OnMovementFinished;
//         }
//
//         private void UpdateDirection(Vector3 direction)
//         {
//             _movementEngine.SetDirection(direction);
//             _rotationEngine.SetDirection(direction);
//         }
//
//         private void OnIsAliveChanged(bool isAlive)
//         {
//             if (!isAlive)
//             {
//                 _stateMachine.SwitchState(PlayerStateType.Dead);
//             }
//         }
//         
//         private void OnMovementFinished()
//         {
//             _stateMachine.SwitchState(PlayerStateType.Idle);
//         }
//     }
// }