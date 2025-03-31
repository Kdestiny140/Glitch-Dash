using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;      // Assign in Inspector
    public float leftToRightInterval = 2f; // Time before left-to-right spawn
    public float topToBottomInterval = 3f; // Time before top-to-bottom spawn
    public float spawnHeightLeft = 1.5f;   // Y position for left-to-right
    public float spawnHeightTop = 4f;      // Y position for top-to-bottom
    public float spawnXLeft = -3f;         // X start for left-to-right (left edge)
    public float spawnXTopMin = -3f;       // Min X for top-to-bottom (left edge)
    public float spawnXTopMax = 0f;        // Max X for top-to-bottom (right edge)

    void Start()
    {
        if (obstaclePrefab == null)
        {
            Debug.LogError("Obstacle Prefab is not assigned in the Inspector!");
            return;
        }

        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            // Spawn left-to-right obstacle from left edge
            Vector3 spawnPosLeft = new Vector3(spawnXLeft, spawnHeightLeft, 0f);
            SpawnObstacle(spawnPosLeft, Vector3.right); // Move right
            yield return new WaitForSeconds(leftToRightInterval);

            // Spawn top-to-bottom obstacle across full width
            float randomX = Random.Range(spawnXTopMin, spawnXTopMax);
            Vector3 spawnPosTop = new Vector3(randomX, spawnHeightTop, 0f);
            SpawnObstacle(spawnPosTop, Vector3.down); // Move down
            yield return new WaitForSeconds(topToBottomInterval);
        }
    }

    void SpawnObstacle(Vector3 position, Vector3 direction)
    {
        GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity);
        if (obstacle == null)
        {
            Debug.LogError("Failed to instantiate obstacle!");
            return;
        }

        ObstacleMovement obstacleMovement = obstacle.GetComponent<ObstacleMovement>();
        if (obstacleMovement != null)
        {
            obstacleMovement.moveSpeed = Random.Range(2f, 4f);
            obstacleMovement.moveDirection = direction;
        }
        else
        {
            Debug.LogError("Obstacle prefab needs an ObstacleMovement script!");
        }
    }
}