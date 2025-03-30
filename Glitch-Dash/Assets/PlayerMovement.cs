using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGrounded;
    public bool gameOver = false;

    public Transform groundCheck;   // Reference to the ground check position
    public float groundDistance = 0.2f;  // Distance for ground check
    public LayerMask groundLayer;  // Layer mask for the ground

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            rb.MovePosition(transform.position + movement); // Use Rigidbody's MovePosition

            // Jump (spacebar)
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
    }

    // Ground detection using a small check
    void FixedUpdate()
    {
        // Raycast downwards to check if the player is grounded
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundLayer);
    }
}
