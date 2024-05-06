using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Update is called once per frame
    private void Update()
    {
        // Move the direction of the player based on the movement direction
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        animator.SetFloat("Velocity", Mathf.Abs(horizontal) + Mathf.Abs(vertical));
    }
}
