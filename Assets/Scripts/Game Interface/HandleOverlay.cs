using UnityEngine;
using System.Collections;

public class HandleOverlay : MonoBehaviour {

    GameObject overlay;

    public void SetOverlay(GameObject _overlay)
    {
        overlay = _overlay;
    }

    public GameObject GetOverlay()
    {
        return overlay;
    }

    public void DestroyOverlay()
    {
        if (overlay != null) {
            Destroy(overlay);
            overlay = null;
        }
    }

}
