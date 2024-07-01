using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public GameObject startMenuPanel;
    public GameObject settingMenuPanel;
    public GameObject promptPanel;
    public AudioSource selectSE;
    public AudioSource backSE;

    public void StartGame()
    {
        // Load the main game scene
        selectSE.Play();
        SceneManager.LoadScene("MainScene");
    }

    public void OpenSettings()
    {
        // Open the settings menu
        selectSE.Play();
        startMenuPanel.SetActive(false);
        settingMenuPanel.SetActive(true);
    }

    public void BackToStartMenu()
    {
        // Return to the start menu from the settings menu
        backSE.Play();
        settingMenuPanel.SetActive(false);
        startMenuPanel.SetActive(true);
    }

    public void ExitGame()
    {
        // Exit the game
        selectSE.Play();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
