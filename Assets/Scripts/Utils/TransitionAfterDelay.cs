using EasyTransition;
using UnityEngine;
using Utils;

public class TransitionAfterDelay : MonoBehaviour
{
    [SerializeField] private TransitionSettings transitionSettings;
    [StringInList(typeof(PropertyDrawersHelper), "AllSceneNames")]
    [SerializeField] private string transitionTo;
    [Range(0, 20f)]
    [SerializeField] private float transitionDelay;

    void Start()
    {
        TransitionManager.Instance().Transition(transitionTo, transitionSettings, transitionDelay);
    }
}
