using Core;
using Model;

namespace Gameplay
{
    public class PlayerGroundState : Simulation.Event<PlayerGroundState>
    {
        private readonly ABEModel model = Simulation.GetModel<ABEModel>();
        public bool isGrounded;

        public override void Execute()
        {
            model.playerController.isGrounded = isGrounded;
        }
    }
}