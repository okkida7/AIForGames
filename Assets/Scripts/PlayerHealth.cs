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
    public PlayerController player;

    public void Update(){
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
}