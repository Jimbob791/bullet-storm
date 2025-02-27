using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class SoundSettings : MonoBehaviour
{
    public Slider soundSlider;
    public AudioMixer audioMixer;

    private void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 50));
    }

    public void SetVolume(float value)
    {
        if (value < 1)
        {
            value = 0.001f;
        }

        RefreshSlider(value);
        PlayerPrefs.SetFloat("SavedMasterVolume", value);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(value/100) * 20f);
    }

    public void SetVolumeFromSlider()
    {
        SetVolume(soundSlider.value);
    }

    public void RefreshSlider(float value)
    {
        soundSlider.value = value;
    }
}
