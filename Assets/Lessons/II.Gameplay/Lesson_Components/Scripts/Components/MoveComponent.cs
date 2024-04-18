using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class MoveComponent : MonoBehaviour, IMoveComponent
    {
        [SerializeField] private Transform _root;
        [SerializeField] private float _speed = 3f;

        public void Move(Vector3 direction)
        {
            _root.position += direction * _speed * Time.deltaTime;
        }
    }
}
