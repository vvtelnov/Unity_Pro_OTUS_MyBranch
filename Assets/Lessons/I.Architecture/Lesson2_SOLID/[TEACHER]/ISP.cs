// using UnityEngine;
//
// namespace Lessons.Architecture
// {
//     public interface IMoveComponent
//     {
//         void Move(Vector3 vector);
//     }
//
//     public interface IDealMeleeDamageComponent
//     {
//         void DealMeleeDamage(Hero hero);
//     }
//    
//     public class EnemyOrc : MonoBehaviour, IMoveComponent, IDealMeleeDamageComponent
//     {
//         [SerializeField]
//         private float moveSpeed;
//     
//         [SerializeField]
//         private int damage;
//     
//         public void Move(Vector3 vector)
//         {
//             this.transform.position += vector * this.moveSpeed;
//         }
//     
//         public void DealMeleeDamage(Hero hero)
//         {
//             hero.HitPoints -= this.damage;
//         }
//     }
//     
//     public sealed class EnemyPlant : MonoBehaviour, IDealMeleeDamageComponent
//     {
//         [SerializeField]
//         private int damage;
//     
//         public void DealMeleeDamage(Hero hero)
//         {
//             hero.HitPoints -= this.damage;
//         }
//     }
//
//     //ЧТОБЫ МЫ ОБРАЩАЛИСЬ, через интерфейсы
//     public sealed class Example
//     {
//         public void MethodA(GameObject enemy)
//         {
//             Vector3 direction = Vector3.up; 
//             if (enemy.TryGetComponent(out IMoveComponent component))
//             {
//                 component.Move(direction);
//             }
//         }
//
//         public void MethodB(GameObject enemy)
//         {
//             Hero hero = default;
//             if (enemy.TryGetComponent(out IDealMeleeDamageComponent component))
//             {
//                 component.DealMeleeDamage(hero);
//             }
//         }
//     }
// }