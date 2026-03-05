using UnityEngine;

namespace BehaviourTrees
{
    public class PlayerMovement : MonoBehaviour, IMoveInput
    {
        [SerializeField] private CharacterController controller = default;
        [SerializeField] private EasyBinding jump = default;
        [SerializeField] private float speed = default;
        [SerializeField] private float jumpSpeed = default;
        [SerializeField] private float groundedVelocity = default;
        private IMoveInput moveInput;
        private float velocity;

        public void SetMoveInput(IMoveInput moveInput) => this.moveInput = moveInput;

        /// <summary>
        /// Nothing is more permanent than a temporary solution.
        /// </summary>
        public Vector3 GetMovement()
        {
            Vector3 movement = (Input.GetAxisRaw("Horizontal") * transform.right) + (Input.GetAxisRaw("Vertical") * transform.forward);
            movement.Normalize();
            return movement;
        }

        private void Update()
        {
            Move(moveInput.GetMovement(), Time.deltaTime, controller.isGrounded, jump.WasPressed);
        }

        private void Move(Vector3 movement, float interval, bool isGrounded, bool jump)
        {
            if (isGrounded)
            {
                if (jump)
                {
                    velocity = jumpSpeed;
                }
                else
                {
                    velocity = groundedVelocity;
                }
            }
            else
            {
                velocity += Physics.gravity.y * interval;
            }

            controller.Move(speed * interval * movement);
            controller.Move(interval * velocity * Vector3.up);
        }
    }
}
