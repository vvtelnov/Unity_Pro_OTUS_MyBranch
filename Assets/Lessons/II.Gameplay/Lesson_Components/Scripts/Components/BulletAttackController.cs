using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class BulletAttackController : MonoBehaviour
    {
        [SerializeField] private AttackComponent _attackComponent;
        
        private void OnTriggerEnter(Collider other)
        {
            _attackComponent.Attack(other.gameObject);    
        }
    }
}