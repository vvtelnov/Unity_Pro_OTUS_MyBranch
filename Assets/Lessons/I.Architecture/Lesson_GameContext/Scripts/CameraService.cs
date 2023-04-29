using UnityEngine;

namespace Lessons.Architecture.GameContexts
{
    public sealed class CameraService : MonoBehaviour
    {
        public Camera Camera
        {
            get { return this.camera; }
        }

        public Transform CameraTransform
        {
            get { return this.cameraTransform; }
        }

        [SerializeField]
        private Transform cameraTransform;

        [SerializeField]
        private new Camera camera;
    }
}