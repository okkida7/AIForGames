using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinUIManager : MonoBehaviour
{
    public AudioSource selectSE;

    public void RestartGame()
    {
        selectSE.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
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
