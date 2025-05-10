using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGrounded;
    public bool gameOver = false;

    public AudioClip backgroundMusic;
    public AudioClip finishSound;
    private AudioSource audioSource;

    private Vector3 checkpointPosition;
    private bool hasUsedCheckpoint = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Setup Audio
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
        if (!hasUsedCheckpoint)
        {
            checkpointPosition = transform.position;
            hasUsedCheckpoint = true;
            Debug.Log("Checkpoint saved at: " + checkpointPosition);
            Invoke(nameof(RespawnAtCheckpoint), 1f); // Delay respawn
        }
        else if (!gameOver) // Ensure it only triggers once
        {
            gameOver = true;
            Debug.Log("Second death — showing Game Over screen...");
            Invoke(nameof(TriggerGameOver), 1f); // Delay Game Over
        }
    }

    if (collision.gameObject.CompareTag("Finish"))
    {
        if (!gameOver)
        {
            gameOver = true;
            Debug.Log("Finish collision detected, loading Next Level...");
            Object.FindFirstObjectByType<NextLevelUIHandler>().ShowUI(); // Show the next level UI
        }
    }
}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered with: " + other.gameObject.name + " | Tag: " + other.gameObject.tag);

        if (other.CompareTag("Finish"))
        {
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

        if (finishSound != null)
        {
            AudioSource.PlayClipAtPoint(finishSound, transform.position);
        }

        Invoke("LoadNextLevelScene", 1f); // delay to let sound play
    }

void LoadNextLevelScene()
{
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = currentSceneIndex + 1;

    if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
    {
        SceneManager.LoadScene(nextSceneIndex);
        Debug.Log("Loading scene index: " + nextSceneIndex);
    }
    else
    {
        Debug.LogError("No more scenes in Build Settings!");
    }
}

    void RespawnAtCheckpoint()
    {
        gameOver = false;
        rb.linearVelocity = Vector3.zero;
        transform.position = checkpointPosition + new Vector3(0, 1, 0); // lifted to prevent ground collision
        Debug.Log("Respawned at checkpoint.");
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}
