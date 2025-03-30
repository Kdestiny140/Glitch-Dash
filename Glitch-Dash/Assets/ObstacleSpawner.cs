using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab; // This needs to be assigned in the Inspector
    public float spawnInterval = 2f; // Time between spawns
    public float spawnHeight = 1.5f; // The fixed height at which obstacles spawn above the floor (Y coordinate)
    public float spawnBaseX = -3f; // The base X spawn position 
    public float spawnBaseZ = 0f; // The base Z spawn position 
    public float spawnOffsetRange = 0.4f; // Random offset range for spawning around the base position
    
    void Start()
    {
        if (obstaclePrefab == null)
        {
            Debug.LogError("Obstacle Prefab is not assigned in the Inspector!");
            return; // Stop the script if obstaclePrefab is not assigned
        }

        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            // Calculate random spawn position with small offsets based on your base positions
            float randomX = spawnBaseX + Random.Range(-spawnOffsetRange, spawnOffsetRange);
            float randomZ = spawnBaseZ + Random.Range(-spawnOffsetRange, spawnOffsetRange);
            Vector3 spawnPos = new Vector3(randomX, spawnHeight, randomZ);

            // Spawn the obstacle at the calculated position
            GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

            // Give each obstacle a random movement direction (handled by a script attached to the prefab)
            ObstacleMovement obstacleMovement = obstacle.GetComponent<ObstacleMovement>();
            if (obstacleMovement != null)
            {
                obstacleMovement.moveSpeed = Random.Range(2f, 4f); // Adjust move speed per obstacle
            }
            else
            {
                Debug.LogError("Obstacle prefab does not have an ObstacleMovement script attached!");
            }

            // Wait before spawning the next obstacle
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
