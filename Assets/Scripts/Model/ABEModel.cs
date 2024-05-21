using Mechanics;

namespace Model
{
    [System.Serializable]
    public class ABEModel
    {
        public Cinemachine.CinemachineVirtualCamera virtualCamera;

        public Cinemachine.CinemachineVirtualCamera broadcastViewCamera;

        public PlayerController player;

        public GameController gameController;
    }
}