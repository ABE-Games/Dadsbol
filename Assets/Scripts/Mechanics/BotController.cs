using Mechanics;
using UnityEngine;

public class BotController : MonoBehaviour
{
    [Range(0f, 1000f)] public float botSpeed = 5f;

    private float changeDirectionInterval;
    private float elapsedTime = 0f;
    private Vector3 targetDirection;

    private PlayerController player;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        changeDirectionInterval = Random.Range(1f, 5f);
        targetDirection = GetRandomDirection();
    }

    private void Update()
    {
        if (player.isABot)
        {
            // Update elapsed time
            elapsedTime += Time.deltaTime;

            // If it's time to change direction, get a new random direction
            if (elapsedTime >= changeDirectionInterval)
            {
                targetDirection = GetRandomDirection();
                elapsedTime = 0f;
            }

            // Move towards the target direction
            Vector3 inverseTargetDir = new Vector3(-targetDirection.x, 0, targetDirection.z);

            player.configurableJoint.targetRotation = Quaternion.LookRotation(inverseTargetDir);
            player.rigidBody.AddForce(targetDirection * botSpeed);
        }
    }

    private Vector3 GetRandomDirection()
    {
        // Generate a random angle
        float angle = Random.Range(0f, Mathf.PI * 2f);

        // Calculate x and z components of the movement direction using trigonometry
        float x = Mathf.Cos(angle);
        float z = Mathf.Sin(angle);

        // Set the player's velocity to the new direction for animation purposes
        player.velocity = new Vector3(x, 0f, z);

        return new Vector3(x, 0f, z).normalized;
    }

}
