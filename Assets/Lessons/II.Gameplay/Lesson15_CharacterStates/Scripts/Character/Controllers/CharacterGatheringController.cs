using System;
using Entities;
using Lessons.Character.Components;
using Lessons.StateMachines.States;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Character.Controllers
{
    public sealed class CharacterGatheringController : MonoBehaviour
    {
        [SerializeField]
        private MonoEntity character;

        [SerializeField]
        private ResourceObject resourceObject;

        private GatherResourceComponent _gatherResourceComponent;

        private void Start()
        {
            _gatherResourceComponent = character.Get<GatherResourceComponent>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _gatherResourceComponent.StartGather(resourceObject);
            }
        }
    }
}