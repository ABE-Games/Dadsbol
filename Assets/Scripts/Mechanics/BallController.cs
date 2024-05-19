using UnityEngine;

namespace Mechanics
{
    public class BallController : MonoBehaviour
    {
        [Header("Particle System")]
        public ParticleSystem explosion;

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