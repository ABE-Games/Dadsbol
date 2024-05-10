using UnityEngine;

public class DrawDirection : MonoBehaviour
{
    [SerializeField] private Color color = Color.red;
    private void Update()
    {
        // Draw a line from the bot's position to the target direction
        Debug.DrawRay(transform.position, transform.forward * 2f, color);
    }
}
