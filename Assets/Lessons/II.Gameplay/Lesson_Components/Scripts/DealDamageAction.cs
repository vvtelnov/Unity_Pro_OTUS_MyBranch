using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class DealDamageAction : MonoBehaviour
    {
        [SerializeField] private LifeComponent _lifeComponent;

        public void DealDamage(int damage)
        {
            _lifeComponent.TakeDamage(damage);
        }
    }
}