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

    //==================
    // ANDROID SETTINGS
    //==================

    // Joystick
    public static void SetIsJoystickLeftSide(bool b_isLeftSide)
    {
        if (b_isLeftSide)
            PlayerPrefs.SetInt("JoystickLeftSide", 1);
        else
            PlayerPrefs.SetInt("JoystickLeftSide", 0);
    }

    public static bool IsJoystickLeftSide()
    {
        if (PlayerPrefs.GetInt("JoystickLeftSide", 1) == 1)
            return true;

        return false;
    }

    // Vibration
    public static void SetVibrationOn(bool b_vibration)
    {
        if (b_vibration)
            PlayerPrefs.SetInt("Vibration", 1);
        else
            PlayerPrefs.SetInt("Vibration", 0);
    }

    public static bool IsVibrationOn()
    {
        if (PlayerPrefs.GetInt("Vibration", 1) == 1)
            return true;

        return false;
    }

    //==================
    // WINDOWS SETTINGS
    //==================

    // Controls
    public static void SaveKeyBindings(Dictionary<string, KeyCode> keys)
    {
        KeyBoardBindings.SetForwardKey(keys["ForwardKey"]);
        KeyBoardBindings.SetBackwardKey(keys["BackwardKey"]);
        KeyBoardBindings.SetAttackKey(keys["AttackKey"]);
        KeyBoardBindings.SetChargedAttackKey(keys["ChargedAttackKey"]);
        KeyBoardBindings.SetPauseKey(keys["PauseKey"]);
        KeyBoardBindings.SetZoomInKey(keys["ZoomInKey"]);
        KeyBoardBindings.SetZoomOutKey(keys["ZoomOutKey"]);
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
