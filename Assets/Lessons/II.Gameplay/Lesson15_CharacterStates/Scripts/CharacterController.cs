using Entities;
using UnityEngine;

namespace Lessons.Gameplay.States
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField]
        private MonoEntity character;

        private IMoveComponent moveComponent;
        private IDeathComponent deathComponent;

        private void Start()
        {
            this.moveComponent = this.character.Get<IMoveComponent>();
            this.deathComponent = this.character.Get<IDeathComponent>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.moveComponent.Move(Vector3.forward);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                this.moveComponent.Move(Vector3.back);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.moveComponent.Move(Vector3.left);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                this.moveComponent.Move(Vector3.right);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                this.deathComponent.Death();
            }
        }
    }
}