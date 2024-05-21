using Mechanics;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private PlayerController player;

    private void Start()
    {
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void Update()
    {
        float velocity = Mathf.Abs(player.velocity.x) + Mathf.Abs(player.velocity.z);
        animator.SetFloat("Velocity", Mathf.Abs(player.velocity.x) + Mathf.Abs(player.velocity.z));

        if (velocity > 0)
        {
            player.dustParticleSystem.Play();
        }
    }
}
