using UnityEngine;
using System.Collections;

public static class SettingsData {

    // Graphics
    public static int ScreenHeight;
    public static int ScreenWidth;
    public static bool isFullscreen;
    public static int AmtOfResolution;

    // Sound
    public static void SetMusicVolume(int vol)
    {
        PlayerPrefs.SetInt("MusicVolume", vol);
    }

    // volume as an int
    public static int GetMusicVolume()
    {
        return PlayerPrefs.GetInt("MusicVolume", 100);
    }

    // volume as a value from 0.0 to 1
    public static float GetMusicVolumeRange()
    {
        return (float)(PlayerPrefs.GetInt("MusicVolume", 100)) / 100f;
    }

    public static void SetSFXVolume(int vol)
    {
        PlayerPrefs.SetInt("SFXVolume", vol);
    }

    // volume as an int
    public static int GetSFXVolume()
    {
        return PlayerPrefs.GetInt("SFXVolume", 100);
    }

    // volume as a value from 0.0 to 1
    public static float GetSFXVolumeRange()
    {
        return (float)(PlayerPrefs.GetInt("SFXVolume", 100)) / 100f;
    }

}
