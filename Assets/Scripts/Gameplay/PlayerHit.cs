using Core;
using Mechanics;
using UnityEngine;

namespace Gameplay
{
    public class PlayerHit : Simulation.Event<PlayerThrow>
    {
        public PlayerController player;
        public Animator animator;

        public override void Execute()
        {
            player.controlEnabled = false;
            player.allowGrabbing = false;
            animator.SetBool("Hit", true);
        }
    }
}