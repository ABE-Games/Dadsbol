using EasyTransition;
using UnityEngine;
using UnityEngine.UI;  // Make sure you have this to work with UI elements
using Utils;

public class ControlScene : MonoBehaviour
{
    public TransitionSettings transition;
    [StringInList(typeof(PropertyDrawersHelper), "AllSceneNames")]
    [SerializeField] private string transitionTo;
    [Range(0, 20f)][SerializeField] private float transitionDelay;
    public Image[] images;  // Array to hold your images
    private int currentImageIndex = 0;  // To track the current image index

    void Start()
    {
        ShowImage(currentImageIndex);  // Show the first image on start
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            ProceedToNextImageOrScene();
        }
    }

    void ProceedToNextImageOrScene()
    {
        if (currentImageIndex < images.Length - 1)
        {
            currentImageIndex++;
            ShowImage(currentImageIndex);
        }
        else
        {
            Invoke("LoadScene", 0.5f);
        }
    }

    void ShowImage(int index)
    {
        // Hide all images first
        foreach (Image img in images)
        {
            Animator anim = images[currentImageIndex].GetComponent<Animator>();
            anim.SetBool("Flip", true);
            img.gameObject.SetActive(false);
        }

        // Show the current image
        images[index].gameObject.SetActive(true);
    }

    void LoadScene()
    {
        TransitionManager.Instance().Transition(transitionTo, transition, transitionDelay);
    }
}
