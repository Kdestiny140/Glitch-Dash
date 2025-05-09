using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Needed for Slider and Dropdown
using UnityEngine.Audio; // Needed for AudioMixer

public class MainMenuHandler : MonoBehaviour
{
    public GameObject optionsPanel; // Options UI panel
    public Slider musicVolumeSlider; // Slider for music volume
    public Toggle fullscreenToggle; // Toggle for fullscreen
    public Dropdown resolutionDropdown; // Dropdown for 480p, 760p, 1080p
    public Dropdown qualityDropdown; // Dropdown for High, Medium, Low
    public AudioMixer audioMixer; // Audio Mixer for volume control

    void Start()
    {
        // Hide options panel
        if (optionsPanel != null)
            optionsPanel.SetActive(false);

        // Initialize settings
        InitializeSettings();
    }

    // Initialize UI elements with current settings
    void InitializeSettings()
    {
        // Music volume (reads from AudioMixer)
        if (musicVolumeSlider != null && audioMixer != null)
        {
            float volume;
            audioMixer.GetFloat("MusicVolume", out volume);
            musicVolumeSlider.value = Mathf.Pow(10, volume / 20); // Convert dB to linear
        }

        // Fullscreen
        if (fullscreenToggle != null)
            fullscreenToggle.isOn = Screen.fullScreen;

        // Resolution
        if (resolutionDropdown != null)
        {
            resolutionDropdown.ClearOptions();
            resolutionDropdown.AddOptions(new System.Collections.Generic.List<string> { "480p", "760p", "1080p" });
            // Set current resolution
            Vector2 currentRes = new Vector2(Screen.width, Screen.height);
            if (currentRes == new Vector2(854, 480)) resolutionDropdown.value = 0;
            else if (currentRes == new Vector2(1366, 768)) resolutionDropdown.value = 1;
            else resolutionDropdown.value = 2; // Default to 1080p
            resolutionDropdown.RefreshShownValue();
        }

        // Quality
        if (qualityDropdown != null)
        {
            qualityDropdown.ClearOptions();
            qualityDropdown.AddOptions(new System.Collections.Generic.List<string> { "High", "Medium", "Low" });
            qualityDropdown.value = QualitySettings.GetQualityLevel();
            qualityDropdown.RefreshShownValue();
        }
    }

    // Called by Play button
    public void PlayGame()
    {
        Debug.Log("Starting game...");
        SceneManager.LoadScene("SampleScene"); 
    }

    // Called by Options button
    public void OpenOptions()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(true);
            Debug.Log("Options panel opened");
        }
        else
        {
            Debug.LogError("Options panel not assigned!");
        }
    }

    // Called by Quit button
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    // Called by Back button in options
    public void CloseOptions()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false);
            Debug.Log("Options panel closed");
        }
    }

    // Called by music volume slider
    public void SetMusicVolume(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20); // Linear to dB
            Debug.Log($"Music volume set to {volume}");
        }
    }

    // Called by fullscreen toggle
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log($"Fullscreen: {isFullscreen}");
    }

    // Called by resolution dropdown
    public void SetResolution(int index)
    {
        Vector2 resolution;
        switch (index)
        {
            case 0: resolution = new Vector2(854, 480); break; // 480p
            case 1: resolution = new Vector2(1366, 768); break; // 760p
            default: resolution = new Vector2(1920, 1080); break; // 1080p
        }
        Screen.SetResolution((int)resolution.x, (int)resolution.y, Screen.fullScreen);
        Debug.Log($"Resolution set to {resolution.x}x{resolution.y}");
    }

    // Called by quality dropdown
    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index, true);
        Debug.Log($"Quality set to {qualityDropdown.options[index].text}");
    }
}