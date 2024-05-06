using Mechanics;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!playerController.isABot)
        {
            // Move the direction of the player based on the movement direction
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            animator.SetFloat("Velocity", Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        }
    }
}
