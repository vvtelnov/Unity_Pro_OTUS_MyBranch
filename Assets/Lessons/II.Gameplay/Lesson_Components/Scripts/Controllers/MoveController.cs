using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class MoveController : AbstractMoveController
    {
        [SerializeField] private GameObject _gameObj;
        private IMoveComponent _moveComponent;
        private IRotateComponent _rotateComponent;

        private void Awake()
        {
            _moveComponent = _gameObj.GetComponentInChildren<IMoveComponent>();
            _rotateComponent = _gameObj.GetComponentInChildren<IRotateComponent>();
        }

        protected override void Move(Vector3 direction)
        {
            _moveComponent.Move(direction);
            _rotateComponent.Rotate(direction);
        }
    }
}