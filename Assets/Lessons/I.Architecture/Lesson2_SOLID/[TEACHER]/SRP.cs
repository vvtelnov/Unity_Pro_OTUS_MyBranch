/// Нарушение SRP
// public sealed class Player : MonoBehaviour
// {
//     [SerializeField] private float speed;
//
//     private void Update() {
//         if (Input.GetKey(KeyCode.UpArrow)) {
//             this.Move(Vector3.up);
//         }
//         else if (Input.GetKey(KeyCode.DownArrow)) {
//             this.Move(Vector3.down);
//         }
//         
//         if (Input.GetKey(KeyCode.LeftArrow)) {
//             this.Move(Vector3.left);
//         }
//         else if (Input.GetKey(KeyCode.RightArrow)) {
//             this.Move(Vector3.right);
//         }
//     }
//
//     private void Move(Vector3 direction) {
//         this.transform.position += direction * Time.deltaTime * this.speed;
//         this.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
//     }
// }

///Правильное SRP
// public sealed class Player : MonoBehaviour {
//     
//     [SerializeField] private float speed;
//
//     public void Move(Vector3 direction) {
//         this.transform.position += direction * this.speed;
//         this.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
//     }
// }
//
// public sealed class MoveController : MonoBehaviour
// {
//     [SerializeField] private Player player;
//
//     private void Update() {
//         if (Input.GetKey(KeyCode.UpArrow)) {
//             this.Move(Vector3.up);
//         }
//         else if (Input.GetKey(KeyCode.DownArrow)) {
//             this.Move(Vector3.down);
//         }
//
//         if (Input.GetKey(KeyCode.LeftArrow)) {
//             this.Move(Vector3.left);
//         }
//         else if (Input.GetKey(KeyCode.RightArrow)) {
//             this.Move(Vector3.right);
//         }
//     }
//
//     private void Move(Vector3 direction) {
//         this.player.Move(direction * Time.deltaTime);
//     }
// }

