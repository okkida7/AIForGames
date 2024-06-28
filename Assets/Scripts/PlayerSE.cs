using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSE : MonoBehaviour
{
    public AudioSource soundEffect;
    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerController.isAttacking && !soundEffect.isPlaying)
        {
            soundEffect.Play();
        }
        else if (!playerController.isAttacking && soundEffect.isPlaying)
        {
            soundEffect.Stop();
        }
    }

}
