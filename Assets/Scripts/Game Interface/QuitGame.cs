using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour {

    // for Android's back button
    float timer = 0f;
    bool tappedOnce = false;

    // Update is called every frame
    void Update()
    {
#if UNITY_ANDROID
        if (tappedOnce)
        {
            timer += Time.deltaTime;
            if (timer > 0.5f)
            {
                tappedOnce = false;
                timer = 0f; // reset back
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (tappedOnce)
            {
                Application.Quit();
            }
            else
            {
                tappedOnce = true;
            }
        }
#endif
    }

    public void CloseApplication()
    {
#if !UNITY_ANDROID
        Application.Quit();
#endif
    }

}
