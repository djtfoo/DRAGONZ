using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MatchTimer : MonoBehaviour {

    public int minutes;

    private float seconds;  // the set time converted to seconds

	// Use this for initialization
	void Start () {
        seconds = (float)(minutes * 60);
	}
	
	// Update is called once per frame
	void Update () {
        if (seconds > 0f)
            seconds -= Time.deltaTime;
	}

    public float GetSeconds()
    {
        return seconds;
    }
}
