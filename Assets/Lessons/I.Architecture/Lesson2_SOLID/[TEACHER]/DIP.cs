

// //Неправильное использование:
// public class MoveController : MonoBehaviour {
//     
//     //Зависимость на конкретный класс:
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
//
// //Правильное использование:
// public interface IMovable {
//     void Move(Vector3 direction);
// }
//
// public class MoveController : MonoBehaviour {
//     
//     [SerializeField] private IMovable movable;
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
//         this.movable.Move(direction * Time.deltaTime);
//     }
// }



