using Core;
using EasyTransition;
using Model;
using UnityEngine;
using Utils;

namespace UI
{
    public class GameMenuController : MonoBehaviour
    {
        [Header("Transition Properties")]
        public TransitionSettings transition;
        [StringInList(typeof(PropertyDrawersHelper), "AllSceneNames")]
        [SerializeField] private string transitionTo;
        [Range(0, 20f)][SerializeField] private float transitionDelay;

        private readonly ABEModel model = Simulation.GetModel<ABEModel>();

        public void Start()
        {
            Time.timeScale = model.gameController.gamePaused ? 0 : 1;
        }

        public void TransitionToScene()
        {
            TransitionManager.Instance().Transition(transitionTo, transition, transitionDelay);
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
            model.gameController.gamePaused = true;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
            model.gameController.gamePaused = false;
        }

        public void StopGame()
        {
            Application.Quit();
        }
    }
}