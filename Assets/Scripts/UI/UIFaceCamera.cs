using UnityEngine;

namespace UI
{
    public class UIFaceCamera : MonoBehaviour
    {
        private Transform mainCamera;

        private void Start()
        {
            // Find the main camera in the scene
            mainCamera = Camera.main.transform;

            // Make sure the health bar faces the camera initially
            FaceCamera();
        }

        private void Update()
        {
            // Make sure the health bar faces the camera every frame
            FaceCamera();
        }

        private void FaceCamera()
        {
            // Calculate the direction from the health bar to the camera
            Vector3 directionToCamera = mainCamera.position - transform.position;
            directionToCamera.y = 0f;

            // Make the health bar look in the direction of the camera
            transform.rotation = Quaternion.LookRotation(-directionToCamera);
        }

    }
}