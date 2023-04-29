using System;
using Game.Gameplay.Player;
using GameSystem;
using Lessons.Meta;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Architecture
{
    // /// Нарушение SRP
    // public sealed class Player : MonoBehaviour
    // {
    //     [SerializeField]
    //     private float speed;
    //
    //     private void Update()
    //     {
    //         if (Input.GetKey(KeyCode.UpArrow))
    //         {
    //             this.Move(Vector3.up);
    //         }
    //         else if (Input.GetKey(KeyCode.DownArrow))
    //         {
    //             this.Move(Vector3.down);
    //         }
    //         else if (Input.GetKey(KeyCode.LeftArrow))
    //         {
    //             this.Move(Vector3.left);
    //         }
    //         else if (Input.GetKey(KeyCode.RightArrow))
    //         {
    //             this.Move(Vector3.right);
    //         }
    //     }
    //
    //     private void Move(Vector3 direction)
    //     {
    //         this.transform.position += direction * Time.deltaTime * this.speed;
    //         this.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    //     }
    // }

    // ///Правильное SRP
    // public sealed class Player : MonoBehaviour
    // {
    //     [SerializeField]
    //     private float speed;
    //
    //     public void Move(Vector3 direction)
    //     {
    //         this.transform.position += direction * this.speed;
    //         this.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    //     }
    // }
    //
    // public sealed class MoveController : MonoBehaviour
    // {
    //     [SerializeField]
    //     private Player player;
    //
    //     private void Update()
    //     {
    //         if (Input.GetKey(KeyCode.UpArrow))
    //         {
    //             this.Move(Vector3.up);
    //         }
    //         else if (Input.GetKey(KeyCode.DownArrow))
    //         {
    //             this.Move(Vector3.down);
    //         }
    //         else if (Input.GetKey(KeyCode.LeftArrow))
    //         {
    //             this.Move(Vector3.left);
    //         }
    //         else if (Input.GetKey(KeyCode.RightArrow))
    //         {
    //             this.Move(Vector3.right);
    //         }
    //     }
    //
    //     private void Move(Vector3 direction)
    //     {
    //         this.player.Move(direction * Time.deltaTime);
    //     }
    // }


    // ///ИСКЛЮЧЕНИЕ!
    // public interface IQuestManager
    // {
    //     event Action<IQuest> OnQuestTaken;
    //
    //     event Action<IQuest> OnQuestStarted;
    //
    //     event Action<IQuest> OnQuestCompleted;
    //
    //     event Action<IQuest> OnQuestRewardReceived;
    //
    //     IQuest[] ActiveQuests { get; }
    //
    //     bool CanTakeNewQuest(IQuest quest);
    //
    //     void TakeNewQuest(IQuest quest);
    //
    //     bool CanReceiveReward(IQuest quest);
    //
    //     void ReceiveReward(IQuest quest);
    // }
    
    //
    // //ТУПО БУДЕТ, ЕСЛИ QUEST MANAGER разложить на SRP.
    // public interface IQuestService
    // {
    //     event Action<IQuest> OnQuestStarted;
    //
    //     event Action<IQuest> OnQuestCompleted;
    //
    //     event Action<IQuest> OnQuestRewardReceived;
    //
    //     IQuest[] Quests { get; }
    // }
    
    // public interface IQuestTaker
    // {
    //     event Action<IQuest> OnQuestTaken;
    //
    //     bool CanTakeNewQuest(IQuest quest);
    //
    //     void TakeNewQuest(IQuest quest);
    // }
    //
    // public interface IQuestRewardReceiver
    // {
    //     event Action<IQuest> OnQuestRewardReceived;
    //
    //     bool CanReceiveReward(IQuest quest);
    //
    //     void ReceiveReward(IQuest quest);
    // }
}