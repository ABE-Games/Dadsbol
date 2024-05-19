using UnityEngine;

namespace UI
{
    public class CursorController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private AudioSource clickSFX;

        private Vector3 mousePosition;

        private void Update()
        {
            // Click left mouse button to turn particles on
            // and place them at mouse position
            if (Input.GetMouseButtonDown(0))
            {
                // Convert screen position to world position with depth considered
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    mousePosition = hit.point;
                    particles.transform.position = mousePosition;
                    particles.Play();
                    clickSFX.Play();
                }
            }
        }
    }
}