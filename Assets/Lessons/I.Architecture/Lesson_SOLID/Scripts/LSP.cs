// using System;
// using UnityEngine;
//
// namespace Lessons.Architecture
// {
//     //Нарушение принципа LSP — если такое произошло, то архитектура выстроена неверно!
//     public class Enemy : MonoBehaviour
//     {
//         [SerializeField]
//         private float moveSpeed;
//
//         [SerializeField]
//         private int damage;
//
//         public virtual void Move(Vector3 vector)
//         {
//             this.transform.position += vector * this.moveSpeed;
//         }
//
//         public virtual void DealMeleeDamage(Hero hero)
//         {
//             hero.HitPoints -= this.damage;
//         }
//     }
//     //
//     // public sealed class EnemyPlant : Enemy
//     // {
//     //     public override void Move(Vector3 vector)
//     //     {
//     //         throw new Exception("Plant can't move!"); //Таких сайд-эффектов не должно быть!!!
//     //     }
//     // }
//
//     //Для примера
//     public sealed class Hero
//     {
//         public int HitPoints { get; set; }
//     }
//     
//     ///Правильный подход — сделать два отдельных класса, это будет лучше,
//     /// чем предыдущее, но как сделать более лакончино, смотрим следующий принцип:
//     // public class EnemyOrc : MonoBehaviour
//     // {
//     //     [SerializeField]
//     //     private float moveSpeed;
//     //
//     //     [SerializeField]
//     //     private int damage;
//     //
//     //     public void Move(Vector3 vector)
//     //     {
//     //         this.transform.position += vector * this.moveSpeed;
//     //     }
//     //
//     //     public void DealMeleeDamage(Hero hero)
//     //     {
//     //         hero.HitPoints -= this.damage;
//     //     }
//     // }
//     //
//     // public sealed class EnemyPlant : MonoBehaviour
//     // {
//     //     [SerializeField]
//     //     private int damage;
//     //
//     //     public void DealMeleeDamage(Hero hero)
//     //     {
//     //         hero.HitPoints -= this.damage;
//     //     }
//     // }
// }