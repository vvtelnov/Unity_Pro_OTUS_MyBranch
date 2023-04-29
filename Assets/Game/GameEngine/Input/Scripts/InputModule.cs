using Elementary;
using GameSystem;
using InputModule;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public sealed class InputModule : GameModule
    {
        [GameService, GameElement]
        [ShowInInspector]
        private InputStateManager stateManager = new();

        [GameService]
        [SerializeField]
        private JoystickInput joystick;

        private void Awake()
        {
            this.joystick.enabled = false;
        }

        public override void ConstructGame(GameContext context)
        {
            this.stateManager.AddState(InputStateId.BASE, new InputState_Joystick(this.joystick));
            this.stateManager.AddState(InputStateId.LOCK, new State());
            this.stateManager.AddState(InputStateId.DIALOG, new State());
        }
    }
}