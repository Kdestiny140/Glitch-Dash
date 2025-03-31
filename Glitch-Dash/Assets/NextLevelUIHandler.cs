using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelUIHandler : MonoBehaviour
{
    public GameObject nextLevelUIPanel;

    void Start() { if (nextLevelUIPanel != null) nextLevelUIPanel.SetActive(false); }

    public void ShowUI()
    {
        if (nextLevelUIPanel != null) nextLevelUIPanel.SetActive(true);
        Debug.Log("Next Level UI displayed");
    }

    public void PlayLevelTwo() { SceneManager.LoadScene("Level 2"); } // Adjust to next level
    public void QuitToMainMenu() { SceneManager.LoadScene("Main Menu"); }
}