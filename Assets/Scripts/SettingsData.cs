using UnityEngine;
using System.Collections;

public static class SettingsData {

    public static void SetSoundVolume(int vol)
    {
        PlayerPrefs.SetInt("SoundVolume", vol);
    }

    public static int GetSoundVolume()
    {
        return PlayerPrefs.GetInt("SoundVolume", 100);
    }

}
