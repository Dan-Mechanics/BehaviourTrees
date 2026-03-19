using UnityEngine;

namespace BehaviourTrees
{
    public class PlayerMovement : MonoBehaviour, IMoveInput
    {
        [SerializeField] private CharacterController controller = default;
        [SerializeField] private EasyBinding jump = default;
        [SerializeField] private EasyBinding sprint = default;
        [SerializeField] private float speed = default;
        [SerializeField] private float jumpSpeed = default;
        [SerializeField] private float sprintMultiplyer = default;
        [SerializeField] private float groundedVelocity = default;
        private IMoveInput moveInput;
        private float velocity;

        public Vector3 GetMovement()
        {
            Vector3 movement = (Input.GetAxisRaw("Horizontal") * transform.right) + (Input.GetAxisRaw("Vertical") * transform.forward);
            movement.Normalize();
            return movement;
        }

        public void SetMoveInput(IMoveInput moveInput) => this.moveInput = moveInput;
        private void Update() => Move(Time.deltaTime);

        private void Move(float interval)
        {
            if (controller.isGrounded)
            {
                if (jump.IsHeld)
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

            float mult = (sprint.IsHeld ? sprintMultiplyer : 1f) * speed * interval;
            controller.Move(mult * moveInput.GetMovement());
            controller.Move(interval * velocity * Vector3.up);
        }
    }
}
