// using System;
// using UnityEngine;
//
// namespace Lessons.Gameplay.CharacterStates
// {
//     public interface IUpdateListener
//     {
//         void OnUpdate(float deltaTime);
//     }
//
//     public sealed class UpdateManager : MonoBehaviour
//     {
//         private IUpdateListener[] listeners;
//
//         private void Update()
//         {
//             foreach (var listener in this.listeners)
//             {
//                 listener.OnUpdate(Time.deltaTime);
//             }
//         }
//     }
//         
//     
//         
//     public sealed class StateUpdateOptimized : State, IUpdateListener
//     {
//         private UpdateManager updateManager;
//
//         public override void Enter()
//         {
//             this.updateManager.AddListener(this);
//         }
//
//         public override void Exit()
//         {
//             this.updateManager.RemoveListener(this);
//         }
//
//         public void OnUpdate(float deltaTime)
//         {
//             
//         }
//     }
// }