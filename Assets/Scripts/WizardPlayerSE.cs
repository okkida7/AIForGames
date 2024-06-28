using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardPlayerSE : MonoBehaviour
{
    public AudioSource soundEffect;
    private WizardPlayerController wizardPlayerController;

    void Start()
    {
        wizardPlayerController = GetComponent<WizardPlayerController>();
    }

    void Update()
    {
        if (wizardPlayerController.isAttacking && !soundEffect.isPlaying)
        {
            soundEffect.Play();
        }
        else if (!wizardPlayerController.isAttacking && soundEffect.isPlaying)
        {
            soundEffect.Stop();
        }
    }
}
