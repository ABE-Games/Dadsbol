using Core;
using Mechanics;
using Model;
using UnityEngine;

public class BotController : MonoBehaviour
{
    [Range(0f, 10f)] public float allowCatchRadius = 1f;
    [Range(0f, 10f)] public float catchRadius = 1f;
    public bool willCatch;
    public Vector3 targetDirection;
    public bool inverseRotation;

    private float changeDirectionInterval;
    private float elapsedTimeChangeDir = 0f;
    private float changeCatchDecisionInterval;
    private float elapsedTimeCatchDec = 0f;

    private PlayerController player;
    private readonly ABEModel model = Simulation.GetModel<ABEModel>();
    private bool init = true;

    private void Start()
    {
        player = GetComponent<PlayerController>();
    }

    private void InitDirection()
    {
        if (init)
        {
            changeDirectionInterval = Random.Range(2f, 5f);
            changeCatchDecisionInterval = Random.Range(5f, 10f);
            targetDirection = GetRandomDirection();
            init = false;
        }
    }

    private void FixedUpdate()
    {
        if (model.gameController.gameStart && player.controlEnabled)
        {
            InitDirection();

            // Update elapsed time
            elapsedTimeChangeDir += Time.deltaTime;
            elapsedTimeCatchDec += Time.deltaTime;

            // If it's time to change direction, get a new random direction
            if (elapsedTimeChangeDir >= changeDirectionInterval)
            {
                targetDirection = GetRandomDirection();
                elapsedTimeChangeDir = 0f;
            }

            if (elapsedTimeCatchDec >= changeCatchDecisionInterval)
            {
                willCatch = Random.Range(0, 2) == 0;
                elapsedTimeCatchDec = 0f;
            }

            // Get the gameobject that has entered the catch radius
            Collider[] colliders = Physics.OverlapSphere(player.configurableJoint.gameObject.transform.position, catchRadius);


            foreach (Collider collider in colliders)
            {
                if (!player.teamBot && collider.gameObject.CompareTag("Interactable"))
                {
                    // If the player is not already caught, catch the player
                    Debug.Log($"DEBUG({gameObject.name.ToUpper()}): Ball in range.");

                    Vector3 ballPosition = collider.transform.position;
                    MoveTowardsBall(ballPosition);

                    // If the player is close enough to the ball, catch it
                    bool catchable = Vector3.Distance(player.configurableJoint.gameObject.transform.position, collider.transform.position) <= allowCatchRadius;

                    if (player.allowGrabbing && catchable && willCatch)
                    {
                        Debug.Log($"DEBUG({gameObject.name.ToUpper()}): Ball caught.");

                        BotGrabController botGrabController = GetComponent<BotGrabController>();
                        botGrabController.grabbedObject = collider.gameObject;

                        player.isGrabbing = true;

                        FaceTowardsPlayer();
                    }
                    else
                    {
                        player.isGrabbing = false;
                    }
                }
                else
                {
                    Move();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(player.configurableJoint.gameObject.transform.position, catchRadius);
        }
    }

    private void Move()
    {
        if (!player.isGrabbing)
        {
            // Move towards the target direction
            Vector3 inverseTargetDir = new Vector3(targetDirection.x, targetDirection.y, -targetDirection.z).normalized;
            player.configurableJoint.targetRotation = Quaternion.LookRotation((inverseRotation) ? -targetDirection : targetDirection);
            player.rigidBody.AddForce(inverseTargetDir * player.speed);

            // Debugging
            // Draw the inverse target direction
            Debug.DrawRay(player.configurableJoint.gameObject.transform.position, inverseTargetDir * 2, Color.green);
        }
        else
        {
            player.velocity = Vector3.zero;
        }
    }

    private void MoveTowardsBall(Vector3 ball)
    {
        if (!player.isGrabbing)
        {
            // Follow the ball, make the player face and move towards the ball
            Vector3 ballDirection = (ball - player.configurableJoint.gameObject.transform.position).normalized;
            Vector3 direction = new Vector3(ballDirection.x, ballDirection.y, -ballDirection.z);
            player.configurableJoint.targetRotation = Quaternion.LookRotation(direction);

            player.rigidBody.AddForce(ballDirection * player.speed);

            // Debugging
            // Draw the inverse target direction
            Debug.DrawRay(player.configurableJoint.gameObject.transform.position, ballDirection * 2, Color.magenta);
        }
        else
        {
            player.velocity = Vector3.zero;
        }
    }

    private void FaceTowardsPlayer()
    {
        Vector3 playerDirection = (model.player.transform.position - player.configurableJoint.gameObject.transform.position).normalized;
        Vector3 direction = new Vector3(playerDirection.x, playerDirection.y, -playerDirection.z);
        player.configurableJoint.targetRotation = Quaternion.LookRotation(direction);
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
