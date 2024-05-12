using EasyTransition;
using UI;
using UnityEngine;
using Utils;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// This scene name is used to determine the current scene in the game.
    /// This will determine whether or not to show the game champion screen or 
    /// just a normal level win.
    /// </summary>
    [Header("Street")]
    [StringInList(typeof(PropertyDrawersHelper), "AllSceneNames")]
    public string currentScene;

    /// <summary>
    /// This transition settings is used to transition between scenes.
    /// This will be used to transition between the game over screen, the game
    /// winner screen, and the next level screen. 
    /// </summary>
    [Header("Scene Transitions")]
    public TransitionSettings transition;
    [StringInList(typeof(PropertyDrawersHelper), "AllSceneNames")]
    [SerializeField] private string nextLevelScene;
    [StringInList(typeof(PropertyDrawersHelper), "AllSceneNames")]
    [SerializeField] private string gameOverScene;
    [StringInList(typeof(PropertyDrawersHelper), "AllSceneNames")]
    [SerializeField] private string gameWinnerScene;
    [Range(0, 20f)][SerializeField] private float transitionDelay;

    [Header("Game Settings")]
    public bool finishedTeamCloseup;
    public bool gameOver;
    public bool gameWon;
    public bool gamePaused;
    public bool gameStart;

    [Header("Cinematic Introduction Controls")]
    public CountdownTimer initialCountdownText;
    private bool invokedStartCountdown = false;

    private void Update()
    {
        // 1. Start the cinematic camera action.
        // 2. After the cinematic camera action, start the countdown.
        if (finishedTeamCloseup)
        {
            // 3. After the countdown, start the game.
            StartTimer();

            if (!initialCountdownText.timeRun)
            {
                initialCountdownText.gameObject.SetActive(false);

                // The countdown is finished. Start the game.
                gameStart = true;
            }
        }

    }

    private void StartTimer()
    {
        // This is to prevent the countdown from starting multiple times.
        // This will only start the countdown once. Since this is called in the Update method.
        if (!invokedStartCountdown)
        {
            initialCountdownText.gameObject.SetActive(true);
            initialCountdownText.timeRun = true;
            invokedStartCountdown = true;
        }
    }

    public void GameOver()
    {
        TransitionManager.Instance().Transition(gameOverScene, transition, transitionDelay);
    }

    public void GameWon()
    {
        // If the current scene contains the word [2.3] then we know that the
        // player has won the game and became champion.
        if (currentScene.Contains("2.3"))
        {
            TransitionManager.Instance().Transition(gameWinnerScene, transition, transitionDelay);
        }
        else
        {
            // Show a normal level win screen.
            // ...

            // Transition to the next level.
            TransitionManager.Instance().Transition(nextLevelScene, transition, transitionDelay);
        }
    }
}
