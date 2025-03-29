using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;  // Prefab for the obstacles
    public float spawnRate = 1f;       // Rate at which obstacles spawn (lower = slower)
    public float moveSpeed = 3f;       // Speed at which obstacles move
    private float nextSpawnTime;

    public float groundY = 0f;         // Ground level (Y position) - Adjust this for desired height
    public float spawnHeightMin = 0f;  // Minimum Y spawn position
    public float spawnHeightMax = 1f;  // Maximum Y spawn position

    public float xMin = -2.95f;        // Minimum X spawn position
    public float xMax = 0f;            // Maximum X spawn position

    public float zMin = -0.98f;        // Minimum Z spawn position
    public float zMax = -0.98f;        // Maximum Z spawn position (fixed between 0 and -0.98)

    void Update()
    {
        // Only spawn obstacles based on the spawn rate
        if (Time.time > nextSpawnTime)
        {
            SpawnObstacle();
            nextSpawnTime = Time.time + spawnRate; // Adjust spawn rate (next spawn time)
        }
    }

    void SpawnObstacle()
    {
        // Randomize X position for obstacle spawn (within a range on the ground)
        float randomX = Random.Range(xMin, xMax);

        // Randomize Y position (within a defined height range)
        float randomY = Random.Range(spawnHeightMin, spawnHeightMax);

        // Randomize Z position: either 0 or -0.98
        float randomZ = Random.Range(0f, 2f) > 1f ? 0f : -0.98f;

        // Define spawn position (X, Y, Z)
        Vector3 spawnPos = new Vector3(randomX, randomY, randomZ);

        // Randomize obstacle direction (left to right, right to left)
        int direction = Random.Range(0, 2); // 0 = left to right, 1 = right to left
        Vector3 velocity;

        // Set obstacle movement direction based on the spawn direction
        if (direction == 0)  // Left to right
        {
            velocity = new Vector3(moveSpeed, 0, 0); // Moving right
        }
        else  // Right to left
        {
            velocity = new Vector3(-moveSpeed, 0, 0); // Moving left
        }

        // Instantiate the obstacle at the spawn position
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
        Rigidbody rb = obstacle.GetComponent<Rigidbody>();

        // Set the velocity and disable gravity for controlled movement
        if (rb != null)
        {
            rb.linearVelocity = velocity;
            rb.useGravity = false; // Disable gravity for this obstacle to move smoothly
        }

        // Destroy obstacle after it moves off-screen
        StartCoroutine(DestroyAfterPassing(obstacle));
    }

    // Coroutine to destroy the obstacle after it moves off-screen
    private IEnumerator DestroyAfterPassing(GameObject obstacle)
    {
        // Keep checking if the obstacle is off-screen (adjust the value based on your screen size)
        while (Mathf.Abs(obstacle.transform.position.x) < 5f)
        {
            yield return null; // Wait for the next frame
        }

        // Destroy the obstacle once it goes out of bounds
        Destroy(obstacle);
    }
}
