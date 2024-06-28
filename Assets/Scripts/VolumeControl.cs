using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider soundEffectVolumeSlider;
    public AudioSource musicSource;

    private void Start()
    {
        // Load the saved volume from PlayerPrefs or set to a default value
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        musicVolumeSlider.value = savedVolume;
        musicSource.volume = savedVolume;

        // Add a listener to the slider to handle changes in the volume
        musicVolumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        // Load the saved volume from PlayerPrefs or set to a default value
        float savedSEVolume = PlayerPrefs.GetFloat("SoundEffectVolume", 0.5f);
        soundEffectVolumeSlider.value = savedSEVolume;

        // Add a listener to the slider to handle changes in the volume
        soundEffectVolumeSlider.onValueChanged.AddListener(OnSEVolumeChanged);
    }

    private void OnVolumeChanged(float volume)
    {
        // Set the volume of the AudioSource
        musicSource.volume = volume;

        // Save the volume to PlayerPrefs
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    private void OnSEVolumeChanged(float volume)
    {
        // Save the volume to PlayerPrefs
        PlayerPrefs.SetFloat("SoundEffectVolume", volume);
    }
}
