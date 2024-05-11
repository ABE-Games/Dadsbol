using Core;
using Gameplay;
using Model;
using System.Collections;
using UnityEngine;
using static Core.Simulation;

public class PlayerGrabController : MonoBehaviour
{
    [SerializeField] private Transform objectPlacement;
    private GameObject grabbedObject;
    [SerializeField] private bool isGrabbing;

    private readonly ABEModel model = Simulation.GetModel<ABEModel>();

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            isGrabbing = true;
            if (grabbedObject != null)
            {
                Debug.Log("DEBUG: Grabbing " + grabbedObject.gameObject.name);

                grabbedObject.transform.position = objectPlacement.position;
                grabbedObject.transform.rotation = objectPlacement.rotation;
                grabbedObject.transform.parent = objectPlacement;

                // Disable the rigidbody of the object so it doesn't fall
                Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
                rb.isKinematic = true;
            }

            model.player.animator.SetBool("Grabbing", true);
        }
        else
        {
            isGrabbing = false;
            if (grabbedObject != null)
            {
                Debug.Log("DEBUG: Not grabbing " + grabbedObject.gameObject.name);

                // Enable the rigidbody of the object so it can fall
                Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                grabbedObject.transform.parent = null;

                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    var ev = Schedule<PlayerThrow>();
                    ev.direction = objectPlacement;
                    ev.objectRigidBody = rb;

                    StartCoroutine(ReleaseGrabbedObject());
                }
            }

            model.player.animator.SetBool("Grabbing", false);
        }
    }

    private IEnumerator ReleaseGrabbedObject()
    {
        yield return new WaitForSeconds(1f);
        if (grabbedObject != null)
        {
            grabbedObject = null;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Interactable") && !isGrabbing)
        {
            grabbedObject = collider.gameObject;
        }
    }
}
