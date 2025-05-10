using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float moveSpeed = 15f;           // Set by ObstacleSpawner
    public Vector3 moveDirection = Vector3.right; // Default, overridden by spawner

    void Update()
    {
        // Move in the assigned direction (no destruction)
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}