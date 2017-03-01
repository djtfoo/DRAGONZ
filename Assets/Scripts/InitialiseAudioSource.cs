using UnityEngine;
using System.Collections;

public class InitialiseAudioSource : MonoBehaviour {

    public AudioSource audioSource;

	// Use this for initialization
	void Start () {
        // set volume
        audioSource.volume = SettingsData.GetSFXVolumeRange();

        // play
        audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
