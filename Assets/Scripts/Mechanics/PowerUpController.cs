using Gameplay;
using System.Collections;
using UnityEngine;
using static Core.Simulation;

namespace Mechanics
{
    public enum PowerUps
    {
        AntMan,
        PitchersHand,
        Megamind,
        Imune,
    }

    public class PowerUpController : MonoBehaviour
    {
        public PowerUps powerUp;
        private Animator animator;

        [Header("Audio")]
        public AudioClip collectedSFX;
        public AudioSource audioSrc;

        private void Start()
        {
            // Randomize which power-up to use
            powerUp = (PowerUps)Random.Range(0, 4);
            animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player Parts"))
            {
                animator.SetTrigger("Collected");
                audioSrc.clip = collectedSFX;
                audioSrc.PlayOneShot(collectedSFX);

                Transform collTransform = GetRootParent(other.transform);
                if (collTransform != null)
                {
                    var ev = Schedule<PlayerCollectPowerUp>();
                    var player = collTransform.GetComponent<PlayerController>();
                    ev.player = player;
                    ev.powerUps = powerUp;

                    StartCoroutine(StopPowerup(player));
                }

                var hiddenLayer = LayerMask.NameToLayer("Hidden");
                gameObject.layer = hiddenLayer;
            }
        }

        private IEnumerator StopPowerup(PlayerController player)
        {
            yield return new WaitForSeconds(player.powerUpCooldown);
            player.animator.SetBool("Shrink", false);
            player.throwForce = 10f;
            player.animator.SetBool("Megamind", false);
            player.isImune = false;
            player.imuneParticleSystem.gameObject.SetActive(false);
        }

        Transform GetRootParent(Transform child)
        {
            Transform parent = child;
            while (parent.parent != null)
            {
                if (parent.parent.CompareTag("Player"))
                {
                    return parent.parent;
                }
                parent = parent.parent;
            }

            return null;
        }
    }
}