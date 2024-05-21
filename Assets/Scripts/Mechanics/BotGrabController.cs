using Core;
using Mechanics;
using Model;
using System.Collections;
using UnityEngine;

public class BotGrabController : MonoBehaviour
{
    [Range(0f, 10f)]
    public float releaseDelay = 2f;
    [SerializeField] private Transform objectPlacement;
    [HideInInspector] public GameObject grabbedObject;
    private PlayerController player;

    private readonly ABEModel model = Simulation.GetModel<ABEModel>();

    private void Start()
    {
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (player.isGrabbing && player.allowGrabbing)
        {
            if (grabbedObject != null)
            {
                Debug.Log("DEBUG: Grabbing " + grabbedObject.gameObject.name);

                // Make the grabbedObject tag to be default
                grabbedObject.tag = "Interactable (Grabbed)";

                grabbedObject.transform.position = objectPlacement.position;
                grabbedObject.transform.rotation = objectPlacement.rotation;
                grabbedObject.transform.parent = objectPlacement;

                // Disable the rigidbody of the object so it doesn't fall
                Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
                rb.isKinematic = true;

                StartCoroutine(ReleaseGrabbedObject(rb));
            }

            player.animator.SetBool("Grabbing", true);
        }
        else
        {
            if (grabbedObject != null)
            {
                Debug.Log("DEBUG: Not grabbing " + grabbedObject.gameObject.name);

                // Enable the rigidbody of the object so it can fall
                Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                grabbedObject.transform.parent = null;
            }

            player.animator.SetBool("Grabbing", false);
        }
    }

    private IEnumerator ReleaseGrabbedObject(Rigidbody rb)
    {
        // Wait a few seconds before throwing the interactable object
        yield return new WaitForSeconds(releaseDelay);

        if (grabbedObject != null)
        {
            grabbedObject.tag = "Interactable";
            player.allowGrabbing = false;

            rb.isKinematic = false;
            grabbedObject.transform.parent = null;
            rb.AddForce(objectPlacement.forward.normalized * player.throwForce, ForceMode.Impulse);

            grabbedObject = null;

            yield return new WaitForSeconds(1f);
            player.allowGrabbing = true;
        }
    }
}
