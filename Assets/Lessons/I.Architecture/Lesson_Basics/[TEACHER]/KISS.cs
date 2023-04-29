// using System;
// using UnityEngine;
//
// // ReSharper disable ArrangeTypeModifiers
// // ReSharper disable UnusedType.Global
// // ReSharper disable UnusedMember.Global
//
// namespace Lessons.Architecture.Basics
// {
//     interface ICraftSystem
//     {
//         event Action<ICraftArgs, ICraftResult> OnCrafted;
//
//         bool CanCraft(ICraftArgs args);
//
//         bool Craft(ICraftArgs args, out ICraftResult result);
//
//         void AddCondition(ICraftCondition condition);
//
//         void RemoveCondition(ICraftCondition condition);
//
//         void AddHandler(ICraftHandler handler);
//
//         void RemoveHandler(ICraftHandler handler);
//     }
//
//     public interface ICraftHandler
//     {
//         void ProcessCraft(ICraftArgs args);
//     }
//
//     public interface ICraftCondition
//     {
//         bool CanCraft(ICraftArgs args);
//     }
//
//     public interface ICraftArgs
//     {
//     }
//
//     public interface ICraftResult
//     {
//     }
//
//
//     interface IInventoryItemCrafter
//     {
//         event Action<InventoryItem> OnCrafted;
//
//         bool CanCraft(InventoryItemReceipt receipt);
//
//         void Craft(InventoryItemReceipt receipt);
//     }
//
//     public sealed class InventoryItem
//     {
//     }
//
//     public sealed class InventoryItemReceipt : ScriptableObject
//     {
//     }
// }