using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class DealDamageAction : MonoBehaviour
    {
        [SerializeField] private Character _character;

        public void DealDamage(int damage)
        {
            _character.TakeDamage(damage);
        }
    }
}