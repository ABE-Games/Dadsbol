using EasyTransition;
using UnityEngine;
using Utils;

namespace UI
{
    public class Win : MonoBehaviour
    {
        public TransitionSettings transition;
        [StringInList(typeof(PropertyDrawersHelper), "AllSceneNames")]
        [SerializeField] private string transitionTo;
        [Range(0, 20f)][SerializeField] private float transitionDelay;

        public void PlayAgain()
        {
            TransitionManager.Instance().Transition(transitionTo, transition, transitionDelay);
        }
    }
}
