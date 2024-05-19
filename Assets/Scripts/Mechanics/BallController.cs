using UnityEngine;

namespace Mechanics
{
    public class BallController : MonoBehaviour
    {
        public bool onStartScreen;

        [Header("Particle System")]
        public ParticleSystem explosion;

        private void Update()
        {
            if (onStartScreen)
            {
                // Shoot the ball wherever the mouse is pointing and clicked at
                if (Input.GetMouseButtonDown(0))
                {
                    explosion.Play();
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        Rigidbody rb = GetComponent<Rigidbody>();
                        rb.isKinematic = false;
                        rb.AddForce((hit.point - transform.position).normalized * 1000f);
                        onStartScreen = false;
                    }
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (gameObject.CompareTag("Interactable"))
            {
                if (collision.gameObject.CompareTag("Player Parts") || collision.gameObject.CompareTag("Bot Parts"))
                {
                    explosion.Play();

                    // Explode the collided object above
                    Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                    rb.AddExplosionForce(50000f, collision.contacts[0].point, 10f);

                    // Player/Bot hit
                    Transform collTransform = GetRootParent(collision.transform);
                    if (collTransform != null)
                    {
                        var playerController = collTransform.GetComponent<PlayerController>();
                        playerController.isHit = true;
                    }
                }
            }
        }

        Transform GetRootParent(Transform child)
        {
            // Get the root parent of the child with the tag named 'Bot'
            Transform parent = child;
            while (parent.parent != null)
            {
                if (parent.parent.CompareTag("Player") || parent.parent.CompareTag("Bot"))
                {
                    return parent.parent;
                }
                parent = parent.parent;
            }

            return null;
        }
    }
}