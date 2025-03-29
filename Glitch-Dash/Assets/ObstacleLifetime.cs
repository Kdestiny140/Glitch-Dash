using UnityEngine;

public class ObstacleLifetime : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 velocity = rb.linearVelocity;

        if (velocity.x > 0 && transform.position.x > 5f) // Left to Right
        {
            Destroy(gameObject);
        }
        else if (velocity.x < 0 && transform.position.x < -5f) // Right to Left
        {
            Destroy(gameObject);
        }
        else if (velocity.y < 0 && transform.position.y < 0f) // Top to Down
        {
            Destroy(gameObject);
        }
    }
}