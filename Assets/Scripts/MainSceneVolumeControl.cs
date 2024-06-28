using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneVolumeControl : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource[] soundEffectSources;

    private void Start()
    {
        // Load the saved volume from PlayerPrefs
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        musicSource.volume = savedVolume;

        // Set the volume of all sound effect sources
        foreach (AudioSource source in soundEffectSources)
        {
            source.volume = PlayerPrefs.GetFloat("SoundEffectVolume", 0.5f);
        }
    }
}

