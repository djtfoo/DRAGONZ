using UnityEngine;
using System.Collections;

public class CloseScreenOverlay : MonoBehaviour {

    public GameObject toClose;

    public void CloseOverlay(/*AudioSource audioClip*/)
    {
        //audioClip.Play();   // play the clip
        //Destroy(toClose, audioClip.clip.length);  // destroy pop-up only after audio clip finishes

        Destroy(toClose);
    }

}
