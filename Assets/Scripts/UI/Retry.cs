using EasyTransition;
using UnityEngine;
using Utils;

namespace UI
{
    public class Retry : MonoBehaviour
    {
        public TransitionSettings transition;
        [StringInList(typeof(PropertyDrawersHelper), "AllSceneNames")]
        [SerializeField] private string transitionTo;
        [Range(0, 20f)][SerializeField] private float transitionDelay;

        public void RetryGame()
        {
            TransitionManager.Instance().Transition(transitionTo, transition, transitionDelay);
        }
    }
}
