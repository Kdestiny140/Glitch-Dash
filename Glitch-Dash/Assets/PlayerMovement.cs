using UnityEngine;
using UnityEngine.SceneManagement;  // Required for scene loading

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGrounded;
    public bool gameOver = false;
    public AudioClip backgroundMusic;
    private AudioSource audioSource;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Handle background music
        audioSource = gameObject.AddComponent<AudioSource>();
        if (backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Background music not assigned in PlayerMovement!");
        }
    }

    void Update()
    {
        if (!gameOver)
        {
            MovePlayer();
        }
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ).normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name + " | Tag: " + collision.gameObject.tag);

        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            TriggerGameOver();
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Finish collision detected, loading Next Level...");
            TriggerNextLevel();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered with: " + other.gameObject.name + " | Tag: " + other.gameObject.tag);

        if (other.CompareTag("Finish"))
        {
            Debug.Log("Finish trigger detected, loading Next Level...");
            TriggerNextLevel();
        }
    }

    void TriggerGameOver()
    {
        gameOver = true;
        Debug.Log("Loading Game Over 1 scene...");

        if (Application.CanStreamedLevelBeLoaded("Game Over 1"))
        {
            SceneManager.LoadScene("Game Over 1");
        }
        else
        {
            Debug.LogError("Game Over 1 scene not found in Build Settings!");
        }
    }

    void TriggerNextLevel()
    {
        gameOver = true;
        Debug.Log("Attempting to load Next Level scene...");

        if (Application.CanStreamedLevelBeLoaded("Next Level"))
        {
            SceneManager.LoadScene("Next Level");
        }
        else
        {
            Debug.LogError("Next Level scene not found in Build Settings!");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}
