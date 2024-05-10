using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AnimatePulse : MonoBehaviour
    {
        [SerializeField][Range(0f, 10f)] private float speedFade;
        [SerializeField] private Image image;

        private float count;

        private void Update()
        {
            count += speedFade * Time.deltaTime;

            if (image != null)
            {
                image.color = new Color(0.5f, 0.5f, 0.5f, Mathf.Sin(count) * 0.5f);
            }
        }
    }
}