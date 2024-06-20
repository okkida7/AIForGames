using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    public int numOfStamina;
    public Sprite stamina_0;
    public Sprite stamina_1;
    public Sprite stamina_2;
    public Sprite stamina_3;
    public Sprite stamina_4;
    public Sprite stamina_5;
    public PlayerController player;

    void Update()
    {
        if (player != null)
        {
            UpdateStaminaBar();
        }
    }

    void UpdateStaminaBar()
    {
        if (player.stamina > numOfStamina)
        {
            player.stamina = numOfStamina;
        }
        if (player.stamina >= 5)
        {
            this.GetComponent<Image>().sprite = stamina_5;
        }
        else if (player.stamina >= 4)
        {
            this.GetComponent<Image>().sprite = stamina_4;
        }
        else if (player.stamina >= 3)
        {
            this.GetComponent<Image>().sprite = stamina_3;
        }
        else if (player.stamina >= 2)
        {
            this.GetComponent<Image>().sprite = stamina_2;
        }
        else if (player.stamina >= 1)
        {
            this.GetComponent<Image>().sprite = stamina_1;
        }
        else if (player.stamina >= 0)
        {
            this.GetComponent<Image>().sprite = stamina_0;
        }
    }
}
