using System;
using Atomic.Elements;
using Lessons.Lesson_Components.Components;
using Lessons.Lesson_SectionAndVisuals;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class ShootController : MonoBehaviour
    {
        [SerializeField] private Character _character;

        private IAtomicAction _shootRequest;

        private void Awake()
        {
            _shootRequest = _character.Get<IAtomicAction>(ShootAPI.SHOOT_REQUEST);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _shootRequest.Invoke();
            }
        }
    }
}