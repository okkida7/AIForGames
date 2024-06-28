using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public GameObject promptPanel;
    public GameObject playerChoicePanel;
    private bool isPaused = false; 

    void Update()
    {
        if(promptPanel.activeSelf || playerChoicePanel.activeSelf)
        {
            return;
        }
        // Toggle pause menu on Tab key press
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        // Resume the game
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void RestartGame()
    {
        // Restart the game
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        // Exit to start menu
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    private void PauseGame()
    {
        // Pause the game
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
