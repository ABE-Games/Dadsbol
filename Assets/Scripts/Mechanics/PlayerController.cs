using Core;
using Gameplay;
using Model;
using UI;
using UnityEngine;
using UnityEngine.UI;
using static Core.Simulation;

namespace Mechanics
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Movement Controls")]
        public Vector3 velocity;
        [Range(0f, 50f)] public float speed;
        public float originalSpeed;
        [Range(0f, 200f)] public float sprintBoost;
        [Range(0f, 100f)] public float jumpForce;
        [Range(0f, 20f)] public float throwForce;
        public bool isGrounded;
        public bool isHit;
        public bool isImune;
        public bool isBenched;
        public bool controlEnabled;
        [Range(0f, 15f)] public float powerUpCooldown;

        [Header("Player Grab Controls")]
        public bool allowGrabbing;
        public bool isGrabbing;

        [Header("Stamina Controls")]
        public Slider staminaSlider;
        public UIOpacity background;
        public UIOpacity fill;
        [Range(0f, 100f)] public float decreaseSpeed;
        [Range(0f, 100f)] public float recoverySpeed;
        [Range(0f, 10f)] public float staminaBarFadeInDelay;
        public bool depleted;

        [Header("Bot Controls")]
        public bool isABot;
        public bool teamBot;

        [Header("Properties")]
        public Rigidbody rigidBody;
        public Transform cameraTransform;
        public ConfigurableJoint configurableJoint;
        public Animator animator;
        public ParticleSystem dustParticleSystem;
        public ParticleSystem imuneParticleSystem;
        [HideInInspector] public bool teamHitDeduct;

        [Header("SFX")]

        private readonly ABEModel model = Simulation.GetModel<ABEModel>();

        private void Start()
        {
            depleted = false;
            teamHitDeduct = true;
            originalSpeed = speed;
        }

        private void FixedUpdate()
        {
            if (model.gameController.gameStart)
            {
                if (!isABot && controlEnabled && !isBenched)
                {
                    allowGrabbing = true;

                    // Move the direction of the player based on the movement direction
                    float horizontal = Input.GetAxis("Horizontal");
                    float vertical = Input.GetAxis("Vertical");

                    velocity = new Vector3(horizontal, 0, vertical);

                    // Get the forward direction of the camera
                    Vector3 cameraForward = cameraTransform.forward;
                    cameraForward.y = 0f;

                    // Normalize the forward direction to ensure consistent speed
                    cameraForward.Normalize();

                    // Stamina bar listener/handler
                    var ev = Schedule<PlayerStaminaBar>();
                    ev.staminaBarFadeInDelay = staminaBarFadeInDelay;

                    // if a key has been pressed, move the player
                    if (Input.anyKey)
                    {
                        PlayerJump();
                        PlayerMovement(cameraForward);
                        PlayerRotation(velocity.x, velocity.z);
                    }

                    OnIdle(horizontal, vertical);
                }

                if (isHit && !isImune)
                {
                    var ev = Schedule<PlayerHit>();
                    ev.player = this;
                    ev.animator = animator;
                }
                else
                {
                    controlEnabled = true;
                    isHit = false;
                }
            }
            else
            {
                controlEnabled = false;
            }
        }

        private void PlayerJump()
        {
            // TODO: jumping does not feel good
            // Jump the player if the space key is pressed
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                dustParticleSystem.Play();
            }
        }

        private void PlayerMovement(Vector3 cameraForward)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rigidBody.AddForce((Input.GetKey(KeyCode.LeftShift) && !depleted)
                    ? ((speed + sprintBoost) * cameraForward)
                    : (speed * cameraForward));
            }

            if (Input.GetKey(KeyCode.A))
            {
                rigidBody.AddForce((Input.GetKey(KeyCode.LeftShift) && !depleted)
                    ? ((sprintBoost + speed) * -cameraTransform.right)
                    : (-cameraTransform.right * speed));
            }

            if (Input.GetKey(KeyCode.S))
            {
                rigidBody.AddForce((Input.GetKey(KeyCode.LeftShift) && !depleted)
                    ? ((speed + sprintBoost) * -cameraForward)
                    : (-speed * cameraForward));
            }
            if (Input.GetKey(KeyCode.D))
            {
                rigidBody.AddForce((Input.GetKey(KeyCode.LeftShift) && !depleted)
                    ? ((sprintBoost + speed) * cameraTransform.right)
                    : (speed * cameraTransform.right));
            }
        }

        private void PlayerRotation(float horizontal, float vertical)
        {
            Vector3 direction = new Vector3(-horizontal, 0, vertical);

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
    }
}
