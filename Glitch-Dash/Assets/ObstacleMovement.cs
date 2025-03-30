using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Control the speed of the obstacle's movement
    private Vector3 direction;
    
    void Start()
    {
        // Randomize the movement direction
        int moveType = Random.Range(0, 3); // 0: left to right, 1: up to down, 2: diagonal movement
        if (moveType == 0)
        {
            direction = new Vector3(1, 0, 0); // Move right
        }
        else if (moveType == 1)
        {
            direction = new Vector3(0, -1, 0); // Move down
        }
        else if (moveType == 2)
        {
            direction = new Vector3(1, -1, 0); // Move diagonal
        }
    }

    void Update()
    {
        // Move the obstacle along the direction vector
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
