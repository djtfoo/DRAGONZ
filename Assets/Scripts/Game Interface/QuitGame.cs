using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour {

    public void CloseApplication()
    {
#if !UNITY_ANDROID
        Application.Quit();
#endif
    }

}
