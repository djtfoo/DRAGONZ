using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioSettingsMenu : MonoBehaviour {

    public Slider musicSlider;
    public Slider SFXSlider;

    // Use this for initialization
    void Start()
    {
        musicSlider.value = SettingsData.GetMusicVolume();
        SFXSlider.value = SettingsData.GetSFXVolume();
    }

    // Update volumes when slider's value change
    public void UpdateMusicVolume()
    {
        AudioManager.instance.SetBGMVolume(musicSlider.value / 100f);
    }

    public void UpdateSFXVolume()
    {
        AudioManager.instance.SetSFXVolume(SFXSlider.value / 100f);
    }

    // Apply button
    public void SaveAudioSettings()
    {
        SettingsData.SetMusicVolume((int)musicSlider.value);
        SettingsData.SetSFXVolume((int)SFXSlider.value);

        AudioManager.instance.SetBGMVolume(SettingsData.GetMusicVolumeRange());
        AudioManager.instance.SetSFXVolume(SettingsData.GetSFXVolumeRange());
    }

    // Cancel button
    public void CancelAudioSettingsChange()
    {
        // reset to what was in PlayerPrefs
        AudioManager.instance.SetBGMVolume(SettingsData.GetMusicVolumeRange());
        AudioManager.instance.SetSFXVolume(SettingsData.GetSFXVolumeRange());
    }

}
