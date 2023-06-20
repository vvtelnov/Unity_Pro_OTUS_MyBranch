// using System;
// using Lessons.Character;
// using Lessons.Engine.Atomic.Values;
// using UnityEngine;
//
// namespace Lessons.StateMachines.States
// {
//     [Serializable]
//     public sealed class DeadState : IState
//     {
//         [SerializeField]
//         private Animator animator;
//
//         void IState.Enter()
//         {
//             animator.SetInteger("State", (int) AnimatorState.Dead);
//         }
//
//         void IState.Exit()
//         {
//             
//         }
//     }
// }