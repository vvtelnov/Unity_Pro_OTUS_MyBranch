using UnityEngine;

namespace Lessons.Architecture.GameSystem
{
    public sealed class CameraFollower : MonoBehaviour
    {
        [SerializeField]
        private Camera targetCamera;

        [SerializeField]
        private Player player;

        [SerializeField]
        private Vector3 offset;

        private void LateUpdate()
        {
            this.targetCamera.transform.position = this.player.GetPosition() + this.offset;
        }
    }
}