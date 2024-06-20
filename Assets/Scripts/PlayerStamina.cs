using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    public Image staminaBarFill;
    private PlayerController playerController;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerController != null)
        {
            UpdateStaminaBar();
        }
    }

    void UpdateStaminaBar()
    {
        float fillAmount = playerController.stamina / playerController.maxStamina;
        staminaBarFill.fillAmount = fillAmount;
    }
}
