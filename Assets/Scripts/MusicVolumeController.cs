using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeController : MonoBehaviour
{
    public AudioSource musicPlayer;
    public Slider volumeSlider;

    private const string VolumePrefsKey = "MusicVolume";

    private float loopDuration = 7.33f;
    private float loopStartTime;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefsKey, 0.5f);
        //float savedVolume = PlayerPrefs.GetFloat(VolumePrefsKey, 40f / 100f);

        if (musicPlayer != null && volumeSlider != null)
        {
            musicPlayer.volume = savedVolume;
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

            // Set the loop start time to the current time
            loopStartTime = Time.time;

            // Start the audio manually
            musicPlayer.Play();
        }
        else
        {
            Debug.LogWarning("Music player or volume slider not assigned!");
        }
    }

    void Update()
    {
        // Check if the audio has reached the end of the loop duration
        if (musicPlayer != null && musicPlayer.isPlaying && Time.time - loopStartTime > loopDuration)
        {
            // Restart the loop
            musicPlayer.time = 0.0f;
            loopStartTime = Time.time;
        }
    }

    void OnVolumeChanged(float volume)
    {
        if (musicPlayer != null)
        {
            musicPlayer.volume = volume;
            PlayerPrefs.SetFloat(VolumePrefsKey, volume);
            PlayerPrefs.Save();
        }
    }
}
