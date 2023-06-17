using System;
using Declarative;
using Elementary;
using Entities;
using Game.GameEngine;
using Game.GameEngine.Mechanics;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Hero
{
    public sealed class HeroModelCharacterState : DeclarativeModel
    {
        [Section]
        public Core core;

        [Section]
        public Components components;

        [Serializable]
        public sealed class Core
        {
            [Section]
            public Main main;

            [Section]
            public Move move;
            
            [Serializable]
            public sealed class Main
            {
                public TransformEngine transformEngine = new();

                [NonSerialized]
                public Variable<IWalkableSurface> walkableSurface = new();
            }
            
            [Serializable]
            public sealed class Move
            {
                [ShowInInspector, ReadOnly]
                public MoveInDirectionMotor moveMotor;

                [Space]
                public FloatVariable speed;
                
                private readonly FixedUpdateMechanics _updateMechanics = new();
                private readonly MoveInDirectionMechanic _moveInDirectionMechanic = new();
                
                [Construct]
                private void ConstructMechanics(Main main)
                {
                    _updateMechanics.Construct(_ => moveMotor.Update());

                    _moveInDirectionMechanic.Construct(main.walkableSurface, main.transformEngine, moveMotor, speed);
                }
            }
        }

        [Serializable]
        public sealed class Components
        {
            [SerializeField]
            private MonoEntityStd entity;

            [SerializeField]
            private Transform movingPivot;
            
            [Construct]
            private void Construct(Core core)
            {
                entity.AddRange(
                    new Component_TransformEngine(core.main.transformEngine),
                    new Component_SetWalkableSurface(core.main.walkableSurface),
                    new Component_MoveInDirection(core.move.moveMotor),
                    new Component_MoveSpeed(core.move.speed),
                    new Component_GetPivot(movingPivot),
                    new Component_ObjectType(ObjectType.HERO)
                );
            }
        }
    }
}