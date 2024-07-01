using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHealth : MonoBehaviour
{
    public int healthAmount = 1;
    public AudioSource pickUpSE;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            WizardPlayerController wizardPlayer= other.GetComponent<WizardPlayerController>();
            if (player != null)
            {
                player.TakeHealth(healthAmount);
                Destroy(gameObject);
            }
            else if (wizardPlayer != null)
            {
                wizardPlayer.TakeHealth(healthAmount);
                Destroy(gameObject);
            }
        }
    }
}
