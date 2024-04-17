using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class MoveComponent : MonoBehaviour , IMoveComponent
    {
        [SerializeField] private Transform _root;
        [SerializeField] private float _speed;
        [SerializeField] private bool _canMove;
        
        public void Move(Vector3 direction)
        {
            if (_canMove)
            {
                _root.position += direction * _speed * Time.deltaTime;
            }
        }
    }
}