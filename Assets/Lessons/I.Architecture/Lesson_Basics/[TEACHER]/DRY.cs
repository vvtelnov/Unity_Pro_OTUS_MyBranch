// // ReSharper disable ArrangeTypeModifiers
// // ReSharper disable ArrangeTypeMemberModifiers
//
// using UnityEngine;
// // ReSharper disable UnusedType.Global
// // ReSharper disable UnusedMember.Local
//
// namespace Lessons.Architecture.Basics
// {
//     //BAD
//     // class Hero
//     // {
//     //     void Move(Vector3 direction)
//     //     {
//     //         //Move logic
//     //     }
//     //
//     //     void Jump()
//     //     {
//     //         //Jump logic
//     //     }
//     //
//     //     void Shoot()
//     //     {
//     //         //Shoot logic
//     //     }
//     // }
//
//     class Enemy
//     {
//         void Move(Vector3 direction)
//         {
//         }
//         
//         void Shoot()
//         {
//             //Shoot logic
//         }
//     }
//
//     class NPC
//     {
//         void Move(Vector3 direction)
//         {
//             //Move logic
//         }
//
//         void Jump()
//         {
//             //Jump logic
//         }
//     }
//     
//     
//
//     
//     
//     
//     interface IEntity
//     {
//         T GetComponent<T>();
//     }
//
//     interface IMoveComponent
//     {
//         void Move(Vector3 direction);
//     }
//     
//     interface IIJumpComponent
//     {
//         void Jump();
//     }
//
//     interface IShootComponent
//     {
//         void Shoot();
//     }
// }