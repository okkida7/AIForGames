using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    private PlayerController playerController;
    private WizardPlayerController wizardPlayerController;
    public GameObject walkThroughTreesButton;
    public GameObject walkThroughWaterButton;
    public GameObject walkThroughOtherButton;
    public GameObject increaseAttackPowerButton;

    public AudioSource selectSE;

    void Update()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        wizardPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<WizardPlayerController>();
    }

    public void OnWalkThroughTreesButtonClicked()
    {
        if(playerController != null){
            playerController.health -= 1;
            playerController.EnableWalkThroughTrees();
        }
        else if(wizardPlayerController != null){
            wizardPlayerController.health -= 1;
            wizardPlayerController.EnableWalkThroughTrees();
        }
        selectSE.Play();
        walkThroughTreesButton.SetActive(false);
    }

    public void OnWalkThroughWaterButtonClicked()
    {
        if(playerController != null){
            playerController.health -= 1;
            playerController.EnableWalkThroughWater();
        }
        else if(wizardPlayerController != null){
            wizardPlayerController.health -= 1;
            wizardPlayerController.EnableWalkThroughWater();
        }
        selectSE.Play();
        walkThroughWaterButton.SetActive(false);
    }

    public void OnWalkThroughOtherButtonClicked()
    {
        if(playerController != null){
            playerController.health -= 1;
            playerController.EnableWalkThroughOther();
        }
        else if(wizardPlayerController != null){
            wizardPlayerController.health -= 1;
            wizardPlayerController.EnableWalkThroughOther();
        }
        selectSE.Play();
        walkThroughOtherButton.SetActive(false);
    }

    public void OnIncreaseAttackPowerButtonClicked()
    {
        if(playerController != null){
            playerController.health -= 1;
            playerController.EnableIncreaseAttackPower();
        }
        else if(wizardPlayerController != null){
            wizardPlayerController.health -= 1;
            wizardPlayerController.EnableIncreaseAttackPower();
        }
        selectSE.Play();
        increaseAttackPowerButton.SetActive(false);
    }
}
