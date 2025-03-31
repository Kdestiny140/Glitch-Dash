using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public GameObject gameOverPanel;

    void Start() 
    { 
        if (gameOverPanel != null) 
            gameOverPanel.SetActive(false); 
    }

    public void ShowGameOver()
    {
        if (gameOverPanel != null) 
            gameOverPanel.SetActive(true);

        Debug.Log("Game Over UI displayed");
    }

    // Restart the game by loading "SampleScene"
    public void TryAgain() 
    { 
        Debug.Log("Restarting game at SampleScene...");
        SceneManager.LoadScene("SampleScene");  
    }

    // Quit to Main Menu
    public void QuitToMainMenu() 
    { 
        Debug.Log("Returning to Main Menu...");
        SceneManager.LoadScene("Main Menu");  
    }
}
