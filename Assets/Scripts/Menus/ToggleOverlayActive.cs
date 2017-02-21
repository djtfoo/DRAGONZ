using UnityEngine;
using System.Collections;

// static boolean "variable"
public static class OverlayActive {

    private static bool b_active;

    public static void SetOverlayActive(bool bActive)
    {
        b_active = bActive;

        //if (bActive)
        //{
        //    PlayerPrefs.SetInt("OverlayActive", 1);
        //}
        //else
        //{
        //    PlayerPrefs.SetInt("OverlayActive", 0);
        //}
    }

    public static bool IsOverlayActive()
    {
        return b_active;

        //int result = PlayerPrefs.GetInt("OverlayActive");   // default is 0 if it's not active
        //if (result == 0)
        //    return false;
        //
        //return true;
    }

}

public class ToggleOverlayActive : MonoBehaviour {

    public void SetActive()
    {
        OverlayActive.SetOverlayActive(true);
    }

    public void SetInactive()
    {
        OverlayActive.SetOverlayActive(false);
    }

}