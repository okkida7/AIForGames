using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PromptManager : MonoBehaviour
{
    public GameObject promptPanel; // Panel containing the input field and button
    public TMP_InputField promptInputField; // Input field for the prompt

    void Start()
    {
        Time.timeScale = 0f; // Pause the game
        promptPanel.SetActive(true); // Show the prompt panel
    }

    public void ShowPrompt()
    {
        string prompt = promptInputField.text;
        if (!string.IsNullOrEmpty(prompt))
        {
            Debug.Log("Prompt: " + prompt); // Handle the prompt here if needed
            StartGame();
        }
    }

    public void StartGame()
    {
        // Hide the prompt panel
        promptPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
