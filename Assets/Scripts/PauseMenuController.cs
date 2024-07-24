using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public GameObject promptPanel;
    public GameObject playerChoicePanel;
    public GameObject abilityPanel;
    private bool isPaused = false; 
    public AudioSource selectSE;

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
        selectSE.Play();
        pauseMenuPanel.SetActive(false);
        abilityPanel.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void RestartGame()
    {
        // Restart the game
        selectSE.Play();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        // Exit to start menu
        selectSE.Play();
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    private void PauseGame()
    {
        // Pause the game
        selectSE.Play();
        pauseMenuPanel.SetActive(true);
        abilityPanel.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
