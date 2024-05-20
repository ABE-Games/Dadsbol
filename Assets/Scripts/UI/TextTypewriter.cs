using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TextTypewriter : MonoBehaviour
    {
        [Range(0.01f, 0.1f)]
        public float typingSpeed = 0.05f;
        public string fullText;
        private string currentText = "";

        private void OnEnable()
        {
            StartCoroutine(TypeText());
            GetComponent<TextMeshProUGUI>().text = "";
        }

        public void PlayText()
        {
            StartCoroutine(TypeText());
            GetComponent<TextMeshProUGUI>().text = "";
        }

        IEnumerator TypeText()
        {
            float startTime = Time.realtimeSinceStartup;


            for (int i = 0; i < fullText.Length; i++)
            {
                currentText = fullText.Substring(0, i + 1);
                GetComponent<TextMeshProUGUI>().text = currentText;

                // Calculate the elapsed time since the last character was typed
                float elapsedTime = Time.realtimeSinceStartup - startTime;
                float waitTime = Mathf.Max(0, typingSpeed - elapsedTime);

                yield return new WaitForSecondsRealtime(waitTime);
                startTime = Time.realtimeSinceStartup;
            }
        }
    }
}