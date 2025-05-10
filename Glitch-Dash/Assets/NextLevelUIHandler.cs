using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelUIHandler : MonoBehaviour
{
    public GameObject nextLevelUIPanel;

    void Start()
    {
        // Hide the next level UI panel at the start
        if (nextLevelUIPanel != null)
            nextLevelUIPanel.SetActive(false);
    }

    // Show the UI panel when called (e.g., after a level is completed)
    public void ShowUI()
    {
        if (nextLevelUIPanel != null)
        {
            nextLevelUIPanel.SetActive(true);
            Debug.Log("Next Level UI displayed");
        }
    }

    // Play the next level based on the current scene index
    public void PlayNextLevel()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Current Scene Index: " + currentSceneIndex);

        // Increment to the next level index (Next level is the next scene in the build)
        int nextSceneIndex = currentSceneIndex + 1;

        // Ensure next scene index is valid and within bounds
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Load the next level
            Debug.Log("Loading Next Scene: " + nextSceneIndex);
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogError("No more levels available.");
        }
    }

    // Quit to main menu
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);  // Load Main Menu (Scene 0)
    }
}
