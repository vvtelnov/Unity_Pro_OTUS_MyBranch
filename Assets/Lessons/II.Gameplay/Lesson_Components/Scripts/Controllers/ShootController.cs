using System;
using Lessons.Lesson_Components.Components;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class ShootController : MonoBehaviour
    {
        [SerializeField] private Character _character;
        private ShootComponent _shootComponent;

        private void Awake()
        {
            _shootComponent = _character.GetComponent<ShootComponent>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _shootComponent.Shoot();        
            }
        }
    }
}