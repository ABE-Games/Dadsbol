using Core;
using Mechanics;
using Model;
using UnityEngine;

namespace Gameplay
{
    public class PlayerHit : Simulation.Event<PlayerThrow>
    {
        public PlayerController player;
        public Animator animator;

        private readonly ABEModel model = Simulation.GetModel<ABEModel>();

        public override void Execute()
        {
            player.controlEnabled = false;
            player.allowGrabbing = false;

            // Make the player invisible
            int invisibleLayer = LayerMask.NameToLayer("Invisible");

            player.gameObject.layer = invisibleLayer;
            Transform[] parts = player.GetComponentsInChildren<Transform>();

            foreach (var part in parts)
            {
                if (part.CompareTag("Bot Parts") || part.CompareTag("Player Parts"))
                {
                    // Set the layer to be "Invisible"
                    part.gameObject.layer = invisibleLayer;
                    DecrementPlayerCount();
                }
            }

            if (!player.isABot)
            {
                model.broadcastViewCamera.gameObject.SetActive(true);
            }

            animator.SetBool("Hit", true);
        }

        private void DecrementPlayerCount()
        {
            if (player.teamHitDeduct)
            {
                if (player.teamBot || !player.isABot) model.gameController.remainingTeamPlayers--;
                else model.gameController.remainingEnemyPlayers--;
                player.teamHitDeduct = false;
            }
        }
    }
}