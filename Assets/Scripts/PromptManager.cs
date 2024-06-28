using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PromptManager : MonoBehaviour
{
    public GameObject promptPanel; // Panel containing the input field and button
    public TMP_InputField promptInputField; // Input field for the prompt
    public GameObject playerChoice;
    public GameObject Swordsman;
    public GameObject Axeman;
    public GameObject Wizard;

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
            ShowChoice();
        }
    }

    public void ShowChoice()
    {
        // Hide the prompt panel
        promptPanel.SetActive(false);
        playerChoice.SetActive(true);
    }

    public void ChoiceSword()
    {
        playerChoice.SetActive(false);
        Swordsman.SetActive(true);
        Time.timeScale = 1f; // Resume the game
    }

    public void ChoiceAxe()
    {
        playerChoice.SetActive(false);
        Axeman.SetActive(true);
        Time.timeScale = 1f; // Resume the game
    }

    public void ChoiceWizard()
    {
        playerChoice.SetActive(false);
        Wizard.SetActive(true);
        Time.timeScale = 1f; // Resume the game
    }
}
