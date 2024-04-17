using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class AttackComponent : MonoBehaviour
    {
        [SerializeField] private int _damage;
        
        public void Attack(GameObject target)
        {
            print("Try attack");
            var lifeComponent = target.GetComponentInParent<ILifeComponent>();
            
            if (lifeComponent == null)
            {
                return;
            }
            
            lifeComponent.TakeDamage(_damage);
        }
    }
}