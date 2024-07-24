using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class PromptManager : MonoBehaviour
{
    private string url = "https://aigame.engineering.nyu.edu/generate";
    public GameObject promptPanel; // Panel containing the input field and button
    public TMP_InputField promptInputField; // Input field for the prompt
    public GameObject playerChoice;
    public GameObject abilityPanel;

    public GameObject Swordsman;
    public GameObject Axeman;
    public GameObject Wizard;

    public GameObject Bandit;
    public GameObject Bat;
    public GameObject Mushroom;
    public GameObject Shade;

    public List<GameObject> enemies;

    public AudioSource selectSE;
    public AudioSource backSE;
    public MapGenerator mapGenerator;

    void Start()
    {
        Time.timeScale = 0f; // Pause the game
        promptPanel.SetActive(true); // Show the prompt panel
        enemies.Add(Bandit);
        enemies.Add(Bat);
        enemies.Add(Mushroom);
        enemies.Add(Shade);
    }

    public void ShowPrompt()
    {
        string prompt = promptInputField.text;
        if (!string.IsNullOrEmpty(prompt))
        {
            selectSE.Play();
            Debug.Log("Prompt: " + prompt); // Handle the prompt here if needed
            StartCoroutine(mapGenerator.PostPrompt(url, prompt));
            ShowChoice();
        } else{
            backSE.Play();
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
        selectSE.Play();
        playerChoice.SetActive(false);
        if(MapGenerator.landPositions.Count > 0){
            int randomIndex = Random.Range(0, MapGenerator.landPositions.Count);
            Instantiate(Swordsman, MapGenerator.landPositions[randomIndex], Quaternion.identity);
            int randomIndexEnemy = Random.Range(0, MapGenerator.landPositions.Count);
            while(randomIndexEnemy == randomIndex){
                randomIndexEnemy = Random.Range(0, MapGenerator.landPositions.Count);
            }
            Instantiate(enemies[Random.Range(0, enemies.Count)], MapGenerator.landPositions[randomIndexEnemy], Quaternion.identity);
            }
        abilityPanel.SetActive(true);
        Time.timeScale = 1f; // Resume the game
    }

    public void ChoiceAxe()
    {
        selectSE.Play();
        playerChoice.SetActive(false);
        if(MapGenerator.landPositions.Count > 0){
            int randomIndex = Random.Range(0, MapGenerator.landPositions.Count);
            Instantiate(Axeman, MapGenerator.landPositions[randomIndex], Quaternion.identity);
            int randomIndexEnemy = Random.Range(0, MapGenerator.landPositions.Count);
            while(randomIndexEnemy == randomIndex){
                randomIndexEnemy = Random.Range(0, MapGenerator.landPositions.Count);
            }
            Instantiate(enemies[Random.Range(0, enemies.Count)], MapGenerator.landPositions[randomIndexEnemy], Quaternion.identity);
        }
        abilityPanel.SetActive(true);
        Time.timeScale = 1f; // Resume the game
    }

    public void ChoiceWizard()
    {
        selectSE.Play();
        playerChoice.SetActive(false);
        if(MapGenerator.landPositions.Count > 0){
            int randomIndex = Random.Range(0, MapGenerator.landPositions.Count);
            Instantiate(Wizard, MapGenerator.landPositions[randomIndex], Quaternion.identity);
            int randomIndexEnemy = Random.Range(0, MapGenerator.landPositions.Count);
            while(randomIndexEnemy == randomIndex){
                randomIndexEnemy = Random.Range(0, MapGenerator.landPositions.Count);
            }
            Instantiate(enemies[Random.Range(0, enemies.Count)], MapGenerator.landPositions[randomIndexEnemy], Quaternion.identity);
        }
        abilityPanel.SetActive(true);
        Time.timeScale = 1f; // Resume the game
    }
}
