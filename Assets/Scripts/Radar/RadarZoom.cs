using UnityEngine;
using System.Collections;

public class RadarZoom : MonoBehaviour {

    public Radar radar;

    public void ZoomIn()
    {
        radar.ZoomIn();
    }

    public void ZoomOut()
    {
        radar.ZoomOut();
    }

}
