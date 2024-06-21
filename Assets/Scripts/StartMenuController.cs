using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public GameObject startMenuPanel;
    public GameObject settingMenuPanel;
    public GameObject promptPanel;

    public void StartGame()
    {
        // Load the main game scene
        SceneManager.LoadScene("MainScene");
    }

    public void OpenSettings()
    {
        // Open the settings menu
        startMenuPanel.SetActive(false);
        settingMenuPanel.SetActive(true);
    }

    public void BackToStartMenu()
    {
        // Return to the start menu from the settings menu
        settingMenuPanel.SetActive(false);
        startMenuPanel.SetActive(true);
    }

    public void ExitGame()
    {
        // Exit the game
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
