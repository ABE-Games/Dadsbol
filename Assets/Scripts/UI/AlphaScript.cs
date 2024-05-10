using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AlphaScript : MonoBehaviour
{
    public float speedFade;
    private float count;
    private Image image;
    void Start()
    {
        
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        count += speedFade * Time.deltaTime;

        if (image != null)
        {
            image.color = new Color(0.5f, 0.5f, 0.5f, Mathf.Sin(count) * 0.5f);
        }
    }
}
