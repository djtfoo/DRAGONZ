using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class SettingsData {

    // Graphics
    public static int ScreenHeight;
    public static int ScreenWidth;
    public static bool isFullscreen;
    public static int AmtOfResolution;
    public static float shadowDistance;
    public static int QualityLevel;

    // Android Settings
    public static bool isLeftSide = true;

    // Controls
    public static void SaveKeyBindings(Dictionary<string, KeyCode> keys)
    {
        KeyBoardBindings.SetForwardKey(keys["ForwardKey"]);
        KeyBoardBindings.SetBackwardKey(keys["BackwardKey"]);
        KeyBoardBindings.SetAttackKey(keys["AttackKey"]);
        KeyBoardBindings.SetChargedAttackKey(keys["ChargedAttackKey"]);
        KeyBoardBindings.SetPauseKey(keys["PauseKey"]);
    }

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
