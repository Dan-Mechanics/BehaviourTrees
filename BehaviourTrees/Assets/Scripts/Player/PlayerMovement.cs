using UnityEngine;

namespace BehaviourTrees
{
    public class PlayerMovement : MonoBehaviour, IMoveInput
    {
        [SerializeField] private CharacterController controller = default;
        [SerializeField] private float speed = default;
        private IMoveInput moveInput;

        private void Start() => moveInput = this;

        public Vector3 GetMovement()
        {
            Vector3 movement = (Input.GetAxisRaw("Horizontal") * transform.right) + (Input.GetAxisRaw("Vertical") * transform.forward);
            movement.Normalize();
            return movement;
        }

        private void Update()
        {
            controller.Move(speed * Time.deltaTime * moveInput.GetMovement());
            controller.Move(Physics.gravity * Time.deltaTime);
        }
    }
}
