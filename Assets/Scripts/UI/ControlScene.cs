using EasyTransition;
using UnityEngine;
using Utils;

public class ControlScene : MonoBehaviour
{
    public TransitionSettings transition;
    [StringInList(typeof(PropertyDrawersHelper), "AllSceneNames")]
    [SerializeField] private string transitionTo;
    [Range(0, 20f)][SerializeField] private float transitionDelay;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            Invoke("LoadScene", 0.5f);
        }

    }

    void LoadScene()
    {
        TransitionManager.Instance().Transition(transitionTo, transition, transitionDelay);
    }

}
