using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene loading

public class MainMenuHandler : MonoBehaviour
{
    // Optional: Reference to an options panel if you add one later
    public GameObject optionsPanel; // Drag your Options UI panel here in Inspector (if you make one)

    void Start()
    {
        // Ensure options panel is hidden at start (if you use one)
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false);
        }
    }

    // Called when Play button is clicked
    public void PlayGame()
    {
        Debug.Log("Starting game..."); // Logs for debugging
        SceneManager.LoadScene("SampleScene"); // Loads your game scene
    }

    // Called when Options button is clicked
    public void OpenOptions()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(true); // Shows options panel if it exists
            Debug.Log("Options panel opened");
        }
        else
        {
            Debug.Log("Options not implemented yet"); // Placeholder message
            // Add options logic here later (e.g., sound, controls)
        }
    }

    // Called when Quit button is clicked
    public void QuitGame()
    {
        Debug.Log("Quitting game..."); // Logs for Editor testing
        Application.Quit(); // Exits the application (works in builds, not Editor)
    }

    // Optional: Close options panel (call from a "Back" button in options)
    public void CloseOptions()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false);
            Debug.Log("Options panel closed");
        }
    }
}