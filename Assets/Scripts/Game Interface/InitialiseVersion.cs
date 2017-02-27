using UnityEngine;
using System.Collections;

public class InitialiseVersion : MonoBehaviour {

    public GameObject windowsVer;
    public GameObject androidVer;

    void Start()
    {
#if UNITY_ANDROID
        windowsVer.SetActive(false);
        androidVer.SetActive(true);
#else
        windowsVer.SetActive(true);
        androidVer.SetActive(false);
#endif
    }

}
