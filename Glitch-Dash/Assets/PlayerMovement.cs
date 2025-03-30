using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Speed of movement
    public float jumpForce = 5f; // Jump force
    private Rigidbody rb; // Rigidbody component for physics-based movement
    private bool isGrounded; // Check if the player is grounded
    public bool gameOver = false; // Game over flag

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Initialize Rigidbody
    }

    void Update()
    {
        if (!gameOver) // Only move if game isn't over
        {
            // Left/Right movement (using A/D or Left/Right)
            float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right
            float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down

            // Move the player using Rigidbody physics (not transform)
            Vector3 movement = new Vector3(moveX, 0, moveZ) * speed * Time.deltaTime;
            rb.MovePosition(transform.position + movement); // Use Rigidbody's MovePosition for smooth movement

            // Jump (spacebar), but only if the player is grounded
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Apply an upward force
                isGrounded = false;  // Player is no longer grounded once they jump
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;  // Player is grounded when colliding with the ground
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;  // Game Over when hitting an obstacle
            Debug.Log("Game Over!");
        }

        // You can add more conditions here for interactions with other objects (e.g., Finish)
    }

    // Optional: Handle collision exit to unground player when in the air
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // Player is no longer grounded if leaving the ground
        }
    }
}
