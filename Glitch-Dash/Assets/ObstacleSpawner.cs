using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnRate = 1f;
    public float moveSpeed = 3f;
    private float nextSpawnTime;

    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            SpawnObstacles();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnObstacles()
    {
        int direction = Random.Range(0, 3); // 0 = left, 1 = right, 2 = top
        Vector3 spawnPos;
        Vector3 velocity;

        if (direction == 0) // Left to Right
        {
            spawnPos = new Vector3(-5f, 1f, 0); // Fixed Y = 1
            velocity = new Vector3(moveSpeed, 0, 0);
        }
        else if (direction == 1) // Right to Left
        {
            spawnPos = new Vector3(5f, 1f, 0); // Fixed Y = 1
            velocity = new Vector3(-moveSpeed, 0, 0);
        }
        else // Top to Down (still from above)
        {
            spawnPos = new Vector3(Random.Range(-4f, 4f), 4f, 0); // Y = 4 for top
            velocity = new Vector3(0, -moveSpeed, 0);
        }

        GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
        Rigidbody rb = obstacle.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = velocity;
            rb.useGravity = false;
        }
    }
}