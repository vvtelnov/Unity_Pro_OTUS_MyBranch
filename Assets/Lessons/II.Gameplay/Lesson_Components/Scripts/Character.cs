using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private RotateComponent _rotateComponent;
        [SerializeField] private LifeComponent _lifeComponent;
        [SerializeField] private FireComponent _fireComponent;

        private void Awake()
        {
            _moveComponent.AppendCondition(()=> !_lifeComponent.IsDead);
            
            _rotateComponent.AppendCondition(()=> !_lifeComponent.IsDead);
            
            _fireComponent.AppendCondition(()=> !_lifeComponent.IsDead);
        }
    }
}
