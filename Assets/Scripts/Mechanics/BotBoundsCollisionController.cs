using UnityEngine;

public class BotBoundsCollisionController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bot Parts"))
        {
            // Get the botController component of the bot
            Transform collTransform = GetRootParent(collision.transform);
            if (collTransform != null)
            {
                BotController botController = collTransform.gameObject.GetComponent<BotController>();

                // Reverse the target facing direction of the bot
                botController.targetDirection = -botController.targetDirection;
            }
        }
    }

    Transform GetRootParent(Transform child)
    {
        // Get the root parent of the child with the tag named 'Bot'
        Transform parent = child;
        while (parent.parent != null)
        {
            if (parent.parent.CompareTag("Bot"))
            {
                return parent.parent;
            }
            parent = parent.parent;
        }

        return null;
    }
}
