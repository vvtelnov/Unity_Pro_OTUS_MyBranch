using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class OneDirectionMoveComponent : MonoBehaviour , IMoveComponent
    {
        [SerializeField] private Transform _root;
        [SerializeField] private float _speed;
        [SerializeField] private bool _canMove;
        
        [ShowInInspector, ReadOnly] 
        private Vector3 _direction;
        
        [Button]
        public void Move(Vector3 direction)
        {
            _direction = direction;
        }

        private void Update()
        {
            if (!_canMove)
            {
                return;
            }
            
            _root.position += _direction * _speed * Time.deltaTime;
        }
    }
}