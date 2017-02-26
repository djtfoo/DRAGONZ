using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public struct SetAudioClip
{
    public string name;
    public AudioClip audioClip;
}

public class AudioManager : MonoBehaviour {

    public AudioSource musicPlayer;

    // music clips
    public SetAudioClip[] musicClipsSerialize;  // to serialize in inspector
    Dictionary<string, AudioClip> musicClips = new Dictionary<string, AudioClip>(); // to access conveniently via name

    // SFX clips
    public SetAudioClip[] SFXClipsSerialize;       // to serialize in inspector
    Dictionary<string, AudioClip> SFXClips = new Dictionary<string, AudioClip>();   // to access conveniently via name


    private void Awake()
    {
        // add music clips to dictionary
        for (int i = 0; i < musicClipsSerialize.Length; ++i)
        {
            musicClips.Add(musicClipsSerialize[i].name, musicClipsSerialize[i].audioClip);
        }
        // add SFX clips to dictionary
        for (int i = 0; i < SFXClipsSerialize.Length; ++i)
        {
            SFXClips.Add(SFXClipsSerialize[i].name, SFXClipsSerialize[i].audioClip);
        }

        // play BGM
        SetBGMVolume(SettingsData.GetMusicVolumeRange());
        PlayMenuBGM();

        DontDestroyOnLoad(this.gameObject);
    }

	// Update is called once per frame
	void Update () {
	
	}

    // generic
    void PlayMusic(string name)
    {
        musicPlayer.clip = musicClips[name];
        musicPlayer.Play();
    }

    void PlaySFX()
    {

    }

    // Set Volume
    public void SetBGMVolume(float _volume)
    {
        musicPlayer.volume = _volume;
    }

    // Play Music
    public void PlayMenuBGM()
    {
        PlayMusic("MenuBGM");
    }

    public void PlayGameBGM()
    {
        PlayMusic("GameBGM");
    }

    // Play SFX

}
