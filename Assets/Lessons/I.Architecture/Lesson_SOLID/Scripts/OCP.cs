// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Game.App;
// using Game.Meta;
//
// namespace Lessons.Architecture
// {
//     //Нарушение принципа OCP.
//     public sealed class UnitAttacker
//     {
//         public void StartAttack(object[] units)
//         {
//             foreach (var unit in units)
//             {
//                 if (unit is IArcher archer)
//                 {
//                     archer.StartShoot();
//                 }
//                 else if (unit is IKnight knight)
//                 {
//                     knight.StartFight();
//                 }
//                 else if (unit is IMage mage)
//                 {
//                     mage.StartSpell();
//                 }
//                 else if (unit is IOrc orc)
//                 {
//                     orc.Cry();
//                 }
//             }
//         }
//     }
//
//     public interface IMage
//     {
//         void StartSpell();
//     }
//
//     public interface IArcher
//     {
//         void StartShoot();
//     }
//
//     public interface IKnight
//     {
//         void StartFight();
//     }
//
//     public interface IOrc
//     {
//         void Cry();
//     }
//
//     //Если мы захотим добавить новый тип врага (ОРКА), то нам придется снова модифицировать Uniy Attacker... 
//
//     ///ПРАВИЛЬНОЕ ИСПОЛЬЗОВАНИЕ
//     public interface IUnit
//     {
//         void StartAttack();
//     }
//     
//     // public sealed class UnitAttacker
//     // {
//     //     public void StartAttack(IUnit[] units)
//     //     {
//     //         foreach (var unit in units)
//     //         {
//     //             unit.StartAttack();
//     //         }
//     //     }
//     // }
//
//
//     ///OCP пример с абстрактным классом из проекта:
//     /// Один раз написали базовую логику и протестировали
//     public abstract class Upgrade
//     {
//         public int Level { get; private set; } = 1;
//
//         public int MaxLevel => this.config.maxLevel;
//
//         public int Price { get; }
//
//         private readonly UpgradeConfig config;
//
//         protected Upgrade(UpgradeConfig config)
//         {
//             this.config = config;
//         }
//
//         public void LevelUp()
//         {
//             if (this.Level >= this.MaxLevel)
//             {
//                 throw new Exception($"Can not increment level for upgrade {this.config.id}!");
//             }
//
//             var nextLevel = this.Level + 1;
//             this.Level = nextLevel;
//             this.OnLevelUp(nextLevel); //Шаблонный метод!
//         }
//
//         protected abstract void OnLevelUp(int level);
//     }
//
//     //Нам не придется каждый раз ползать в Upgrades Manager
//     // public sealed class UpgradesManager
//     // {
//     //     public void LevelUpUpgrade(Upgrade upgrade)
//     //     {
//     //         if (upgrade.Level < upgrade.MaxLevel)
//     //         {
//     //             this.SpendMoney(upgrade.Price);
//     //             upgrade.LevelUp();
//     //         }
//     //     }
//     // }
//
//
//     // /// Затем пишем логику для каждого типа апгрейда
//     // public sealed class DamageUpgrade : Upgrade
//     // {
//     //     public DamageUpgrade(UpgradeConfig config) : base(config)
//     //     {
//     //     }
//     //
//     //     protected override void OnLevelUp(int level)
//     //     {
//     //         //Логика прокачки урона
//     //     }
//     // }
//     // //
//     // public sealed class HitPointsUpgrade : Upgrade
//     // {
//     //     public HitPointsUpgrade(UpgradeConfig config) : base(config)
//     //     {
//     //     }
//     //
//     //     protected override void OnLevelUp(int level)
//     //     {
//     //         //Логика прокачки здоровья
//     //     }
//     // }
//
//     // ///OCP пример с интерфейсами:
//     // ///Один раз написали общую логику загрузки приложения, и нам не придется каждый раз ползать и дописывать код в ApplicationLoader
//     // public sealed class ApplicationLoader
//     // {
//     //     private ILoadingTask[] tasks;
//     //
//     //     public async void LoadApplication()
//     //     {
//     //         foreach (var task in this.tasks)
//     //         {
//     //             await task.Do();
//     //         }
//     //     }
//     // }
//     //
//     // public interface ILoadingTask
//     // {
//     //     Task Do();
//     // }
//
//     // ///Затем реализуем каждый компонент по отдельности
//     // public sealed class LoadingTask_LoadPlayerData : ILoadingTask
//     // {
//     //     public Task Do()
//     //     {
//     //         //Load player data
//     //     }
//     // }
//     
//     // public sealed class LoadingTask_LoadGameScene : ILoadingTask
//     // {
//     //     public Task Do()
//     //     {
//     //         //Load Game Scene
//     //     }
//     // }
// }