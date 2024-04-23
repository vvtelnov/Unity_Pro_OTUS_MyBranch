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

        private IAtomicAction _shootAction;

        private void Awake()
        {
            _shootAction = _character.Get<IAtomicAction>(FireAPI.FIRE_REQUEST);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _shootAction.Invoke();
            }
        }
    }
}