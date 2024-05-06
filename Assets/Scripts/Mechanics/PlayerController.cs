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
        public bool isABot;

        [Header("Properties")]
        public Rigidbody rigidBody;
        public Transform cameraTransform;
        public ConfigurableJoint configurableJoint;

        private void FixedUpdate()
        {
            if (!isABot)
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
                    PlayerJump();
                    PlayerMovement(cameraForward);
                    PlayerRotation(horizontal, vertical);
                }

                OnIdle(horizontal, vertical);
            }
            else
            {
                OnIdle();
            }
        }

        private void PlayerJump()
        {
            // TODO: jumping does not feel good
            // Jump the player if the space key is pressed
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        private void PlayerMovement(Vector3 cameraForward)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rigidBody.AddForce((Input.GetKey(KeyCode.LeftShift))
                    ? (speed * sprintBoost * cameraForward)
                    : (speed * cameraForward));
            }

            if (Input.GetKey(KeyCode.A))
                rigidBody.AddForce(-cameraTransform.right * strafeSpeed);

            if (Input.GetKey(KeyCode.S))
                rigidBody.AddForce(-cameraForward * speed);

            if (Input.GetKey(KeyCode.D))
                rigidBody.AddForce(cameraTransform.right * strafeSpeed);
        }

        private void PlayerRotation(float horizontal, float vertical)
        {
            Vector3 direction = new Vector3(horizontal, 0, -vertical);

            if (direction != Vector3.zero)
            {
                // rotate the player to face the direction of movement
                configurableJoint.targetRotation = Quaternion.LookRotation(direction);
            }
        }

        private void OnIdle(float horizontal, float vertical)
        {
            // NOTE: this is only for the idling drift issue when the player is not moving
            // Freeze the player's rigidbody on idling
            rigidBody.constraints = (horizontal == 0 && vertical == 0)
                ? RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ
                : RigidbodyConstraints.None;
        }

        private void OnIdle()
        {
            // NOTE: this is only for the idling drift issue when the player is not moving
            // Freeze the player's rigidbody on idling
            rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        }
    }
}
