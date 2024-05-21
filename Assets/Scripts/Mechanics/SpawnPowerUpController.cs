using System.Collections;
using UnityEngine;

public class SpawnPowerUpController : MonoBehaviour
{
    public GameObject powerupPrefab;
    public Vector2 delayRange;
    [Range(1, 10)]
    public int powerups;

    private void Start()
    {
        for (int i = 0; i < powerups; i++)
        {
            StartCoroutine(SpawnPowerUp(delayRange.x, delayRange.y));
        }
    }

    private IEnumerator SpawnPowerUp(float from, float to)
    {
        yield return new WaitForSeconds(Random.Range(from, to));

        // Get the BoxCollider component
        var boxCollider = GetComponent<BoxCollider>();

        // Get the bounds of the box collider in world space
        var bounds = boxCollider.bounds;

        // Calculate random position within the bounds
        var x = Random.Range(bounds.min.x, bounds.max.x);
        var y = Random.Range(bounds.min.y, bounds.max.y);
        var z = Random.Range(bounds.min.z, bounds.max.z);
        var position = new Vector3(x, y, z);

        // Instantiate the power-up prefab at the calculated position
        var powerup = Instantiate(powerupPrefab, position, Quaternion.identity);

        // Set the power-up's parent to this game object
        powerup.transform.parent = transform;
    }
}
