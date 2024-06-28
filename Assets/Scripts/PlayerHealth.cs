using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int numOfHearts;
    public Sprite heart_0;
    public Sprite heart_1;
    public Sprite heart_2;
    public Sprite heart_3;
    public Sprite heart_4;
    public Sprite heart_5;
    private PlayerController player;
    private WizardPlayerController wizardPlayer;


    public void Update(){
        if(Time.deltaTime == 0)
        {
            return;
        }
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if(playerObject != null){
            if(playerObject.GetComponent<PlayerController>() != null){
                player = playerObject.GetComponent<PlayerController>();
            }
            else if(playerObject.GetComponent<WizardPlayerController>() != null){
                wizardPlayer = playerObject.GetComponent<WizardPlayerController>();
            }
        }
        if(player == null && wizardPlayer == null){
            return;
        }
        if(player != null){
            UpdateHealthBar();
        }
        else if(wizardPlayer != null){
            UpdateWizardHealthBar();
        }
    }

    public void UpdateHealthBar(){
        if(player.Health>numOfHearts){
            player.Health = numOfHearts;
        }
        if(player.Health == 5){
            this.GetComponent<Image>().sprite = heart_5;
        }
        else if(player.Health == 4){
            this.GetComponent<Image>().sprite = heart_4;
        }
        else if(player.Health == 3){
            this.GetComponent<Image>().sprite = heart_3;
        }
        else if(player.Health == 2){
            this.GetComponent<Image>().sprite = heart_2;
        }
        else if(player.Health == 1){
            this.GetComponent<Image>().sprite = heart_1;
        }
        else if(player.Health == 0){
            this.GetComponent<Image>().sprite = heart_0;
        }
    }

    public void UpdateWizardHealthBar(){
        if(wizardPlayer.Health>numOfHearts){
            wizardPlayer.Health = numOfHearts;
        }
        if(wizardPlayer.Health == 5){
            this.GetComponent<Image>().sprite = heart_5;
        }
        else if(wizardPlayer.Health == 4){
            this.GetComponent<Image>().sprite = heart_4;
        }
        else if(wizardPlayer.Health == 3){
            this.GetComponent<Image>().sprite = heart_3;
        }
        else if(wizardPlayer.Health == 2){
            this.GetComponent<Image>().sprite = heart_2;
        }
        else if(wizardPlayer.Health == 1){
            this.GetComponent<Image>().sprite = heart_1;
        }
        else if(wizardPlayer.Health == 0){
            this.GetComponent<Image>().sprite = heart_0;
        }
    }
}