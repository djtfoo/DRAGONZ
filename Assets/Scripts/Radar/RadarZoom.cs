using UnityEngine;
using System.Collections;

public class RadarZoom : MonoBehaviour {

    public Radar radar;

    private void Start()
    {
#if !UNITY_ANDROID
        this.gameObject.SetActive(false);
#endif
    }

    public void ZoomIn()
    {
        radar.ZoomIn();
    }

    public void ZoomOut()
    {
        radar.ZoomOut();
    }

}
