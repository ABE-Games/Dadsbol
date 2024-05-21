using UnityEngine;

namespace UI
{
    public class WinnerSceneAnimController : MonoBehaviour
    {
        public GameObject menuWin;
        public ParticleSystem confetti;
        public AudioSource winSound;

        public void ShowWinMenu()
        {
            menuWin.SetActive(true);
        }

        public void FireConfetti()
        {
            winSound.Play();
            confetti.Play();
        }
    }
}