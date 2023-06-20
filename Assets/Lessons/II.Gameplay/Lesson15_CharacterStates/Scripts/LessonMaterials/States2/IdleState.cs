// using System;
// using Lessons.Character;
// using UnityEngine;
//
// namespace Lessons.StateMachines.States
// {
//     [Serializable]
//     public sealed class IdleState : IState
//     {
//         [SerializeField]
//         private Animator animator;
//         
//         void IState.Enter()
//         {
//             animator.SetInteger("State", (int) AnimatorState.Idle);
//         }
//
//         void IState.Exit()
//         {
//             
//         }
//     }
// }