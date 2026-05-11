using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private float defaultVolume = 0.5f;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private AudioMixer audioMixer;
    private void Start()
    {
        if(PlayerPrefs.HasKey("MasterVolume") || PlayerPrefs.HasKey("MusicVolume") || PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadVolume();
        }
        else
        {
            masterSlider.value = defaultVolume;
            SetMasterVolume();
            volumeSlider.value = defaultVolume;
            SetNusicVolume();
            sfxSlider.value = defaultVolume;
            SetSFXVolume();
        }
    }
    public void SetNusicVolume()
    {
        float volume = volumeSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    private void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", defaultVolume);
        SetMasterVolume();
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", defaultVolume);
        SetNusicVolume();
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", defaultVolume);
        SetSFXVolume();
    }
}
