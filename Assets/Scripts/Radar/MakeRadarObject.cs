using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MakeRadarObject : MonoBehaviour {

    public Image image;
    public Image iconHigher;
    public Image iconLower;
	// Use this for initialization
	void Start () 
    {
        Radar.RegisterRadarObject(this.gameObject, image,iconHigher,iconLower);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        Radar.RemoveRadarObject(this.gameObject);
    }
}

