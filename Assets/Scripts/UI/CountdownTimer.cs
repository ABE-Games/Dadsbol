using TMPro;
using UnityEngine;


namespace UI
{
    public enum TimerFormat
    {
        Seconds,
        Minutes,
        Hours
    }

    public class CountdownTimer : MonoBehaviour
    {
        [Header("Time UI")]
        public TextMeshProUGUI timerText;
        public TimerFormat format;

        [Header("Time Settings")]

        /// <summary>
        /// The time remaining for the countdown timer in seconds.
        /// </summary>
        [Range(0, 1000f)] public float timeRemaining;
        public bool timeRun = false;
        public bool timePaused = false;

        private float totalTime;

        void Start()
        {
            totalTime = timeRemaining;
            DisplayTime(totalTime);
        }

        void Update()
        {
            if (timeRun && !timePaused)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    DisplayTime(timeRemaining);
                }
                else
                {
                    timeRemaining = 0;
                    timeRun = false;
                }
            }
        }

        void DisplayTime(float timeToDisplay)
        {
            switch (format)
            {
                case TimerFormat.Seconds:
                    timerText.text = timeToDisplay.ToString("F0");
                    break;
                case TimerFormat.Minutes:
                    int minutes = Mathf.FloorToInt(timeToDisplay / 60);
                    int seconds = Mathf.FloorToInt(timeToDisplay % 60);
                    timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                    break;
                case TimerFormat.Hours:
                    int hours = Mathf.FloorToInt(timeToDisplay / 3600);
                    int remainingMinutes = Mathf.FloorToInt((timeToDisplay % 3600) / 60);
                    int remainingSeconds = Mathf.FloorToInt(timeToDisplay % 60);
                    timerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, remainingMinutes, remainingSeconds);
                    break;
            }
        }

        public void StartTimer()
        {
            timeRun = true;
            timePaused = false;
        }

        public void PauseTimer()
        {
            timePaused = true;
        }

        public void ResumeTimer()
        {
            timePaused = false;
        }

        public void ResetTimer()
        {
            timeRemaining = totalTime;
            DisplayTime(timeRemaining);
            timeRun = false;
        }
    }
}
