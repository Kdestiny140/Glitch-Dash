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
    
    int numLeftToRight = 10; // Number of obstacles to spawn at once
    int numTopToBottom = 10;
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
        // Use numLeftToRight here
        for (int i = 0; i < numLeftToRight; i++)
        {
            Vector3 spawnPosLeft = new Vector3(spawnXLeft, spawnHeightLeft, 0f);
            SpawnObstacle(spawnPosLeft, Vector3.right);
            yield return new WaitForSeconds(0.8f);
        }

        yield return new WaitForSeconds(leftToRightInterval);

        // Use numTopToBottom here
        for (int i = 0; i < numTopToBottom; i++)
        {
            float randomX = Random.Range(spawnXTopMin, spawnXTopMax);
            Vector3 spawnPosTop = new Vector3(randomX, spawnHeightTop, 0f);
            SpawnObstacle(spawnPosTop, Vector3.down);
            yield return new WaitForSeconds(0.8f);
        }

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