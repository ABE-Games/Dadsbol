using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [ExecuteAlways]
    public class UIOpacity : MonoBehaviour
    {
        [Range(0, 1f)]
        public float opacity = 1f;
        private Graphic graphic;

        void Start()
        {
            graphic = GetComponent<Graphic>();
        }

        void Update()
        {
#if UNITY_EDITOR
            // Update the opacity of the UI element in Edit mode
            if (!Application.isPlaying)
            {
                SetOpacity(opacity);
            }
#endif
            SetOpacity(opacity);
        }

        // Function to set the opacity of the UI element
        public void SetOpacity(float value)
        {
            // Ensure the opacity value is between 0 and 1
            opacity = Mathf.Clamp01(value);

            // Update the color with the same alpha value but unchanged RGB values
            if (graphic != null)
            {
                Color color = graphic.color;
                color.a = opacity;
                graphic.color = color;
            }
        }
    }
}