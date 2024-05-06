using Gameplay;
using UnityEngine;
using static Core.Simulation;

namespace Mechanics
{
    public class PlayerGroundController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Ground"))
            {
                var ev = Schedule<PlayerGroundState>();
                ev.isGrounded = true;
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.CompareTag("Ground"))
            {
                var ev = Schedule<PlayerGroundState>();
                ev.isGrounded = false;
            }
        }
    }
}