using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class ShootController : MonoBehaviour
    {
        [SerializeField] private Character _character;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _character.Shoot();        
            }
        }
    }
}