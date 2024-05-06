using UnityEngine;

namespace Mechanics
{
    public class PlayerController : MonoBehaviour
    {
        [Range(0, 100f)] public float speed = 10.0f;
        [Range(0, 100f)] public float strafeSpeed;
        [Range(0, 100f)] public float jumpForce = 10.0f;

        public Rigidbody rb;
        public bool isGrounded;


        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    rb.AddForce(rb.transform.forward * speed * 1.5f);
                }
                else
                {
                    rb.AddForce(rb.transform.forward * speed);
                }
                Debug.Log("called");
            }

            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(-rb.transform.forward * speed);
            }

            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(-rb.transform.right * strafeSpeed);
            }

            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(rb.transform.right * strafeSpeed);
            }
        }

    }
}