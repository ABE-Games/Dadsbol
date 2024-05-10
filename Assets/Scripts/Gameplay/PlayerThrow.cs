using Core;
using Model;
using UnityEngine;

namespace Gameplay
{
    public class PlayerThrow : Simulation.Event<PlayerThrow>
    {
        private readonly ABEModel model = Simulation.GetModel<ABEModel>();
        public Transform direction;
        public Rigidbody objectRigidBody;

        public override void Execute()
        {
            objectRigidBody.AddForce(direction.forward.normalized * model.player.throwForce, ForceMode.Impulse);
        }
    }
}