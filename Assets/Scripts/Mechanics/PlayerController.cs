using UnityEngine;

namespace Mechanics
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Movement Controls")]
        [Range(50f, 200f)] public float speed;
        [Range(50f, 200f)] public float strafeSpeed;
        [Range(50f, 200f)] public float sprintBoost;
        [Range(0, 100f)] public float jumpForce;
        public bool isGrounded;

        [Header("Properties")]
        public Rigidbody rigidBody;
        public Transform cameraTransform;
        public ConfigurableJoint configurableJoint;

        private void FixedUpdate()
        {
            // Move the direction of the player based on the movement direction
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Get the forward direction of the camera
            Vector3 cameraForward = cameraTransform.forward;
            cameraForward.y = 0f;

            // Normalize the forward direction to ensure consistent speed
            cameraForward.Normalize();

            // if a key has been pressed, move the player
            if (Input.anyKey)
            {
                rigidBody.AddForce(PlayerMovement(cameraForward));
                PlayerRotation(horizontal, vertical);
            }
        }

        private Vector3 PlayerMovement(Vector3 cameraForward)
        {
            Vector3 moveForce = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                moveForce = (Input.GetKey(KeyCode.LeftShift))
                    ? (speed * sprintBoost * cameraForward)
                    : (speed * cameraForward);
            }

            if (Input.GetKey(KeyCode.A))
                moveForce = -cameraTransform.right * strafeSpeed;

            if (Input.GetKey(KeyCode.S))
                moveForce = -cameraForward * speed;

            if (Input.GetKey(KeyCode.D))
                moveForce = cameraTransform.right * strafeSpeed;

            return moveForce;
        }

        private void PlayerRotation(float horizontal, float vertical)
        {
            Vector3 direction = new Vector3(horizontal, 0, -vertical).normalized;

            if (direction != Vector3.zero)
            {
                // rotate the player to face the direction of movement
                configurableJoint.targetRotation = Quaternion.LookRotation(direction);
            }
        }
    }
}
