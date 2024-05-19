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

    private readonly ABEModel model = Simulation.GetModel<ABEModel>();

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && model.player.controlEnabled && model.player.allowGrabbing)
        {
            model.player.isGrabbing = true;

            if (grabbedObject != null)
            {
                Debug.Log("DEBUG: Grabbing " + grabbedObject.gameObject.name);

                grabbedObject.tag = "Interactable (Grabbed)";
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
            model.player.isGrabbing = false;

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
                    StartCoroutine(RevertTag());
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
            model.player.allowGrabbing = false;
            grabbedObject = null;

            yield return new WaitForSeconds(1f);
            model.player.allowGrabbing = true;
        }
    }

    private IEnumerator RevertTag()
    {
        yield return new WaitForSeconds(0.35f);
        grabbedObject.tag = "Interactable";
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Interactable") && !model.player.isGrabbing)
        {
            grabbedObject = collider.gameObject;
        }
    }
}
