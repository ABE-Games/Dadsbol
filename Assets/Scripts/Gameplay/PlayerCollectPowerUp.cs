using Core;
using Mechanics;
using Model;

namespace Gameplay
{
    public class PlayerCollectPowerUp : Simulation.Event<PlayerThrow>
    {
        public PlayerController player;
        public PowerUps powerUps;

        private readonly ABEModel model = Simulation.GetModel<ABEModel>();

        public override void Execute()
        {
            switch (powerUps)
            {
                case PowerUps.AntMan:
                    player.animator.SetBool("Shrink", true);
                    break;
                case PowerUps.PitchersHand:
                    player.throwForce = 20f;
                    break;
                case PowerUps.Megamind:
                    player.animator.SetBool("Megamind", true);
                    break;
                case PowerUps.Imune:
                    player.isImune = true;
                    player.imuneParticleSystem.gameObject.SetActive(true);
                    break;
            }
        }
    }
}