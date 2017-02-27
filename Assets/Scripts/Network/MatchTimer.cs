using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MatchTimer : NetworkBehaviour {

    public int minutes;

    private float seconds;  // the set time converted to seconds

	// Use this for initialization
	void Start () {
        seconds = (float)(minutes * 60);
	}
	
	// Update is called once per frame
	void Update () {

        GameObject[] playersGO = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < playersGO.Length; ++i)
        {
            if (playersGO[i].GetComponent<PlayerSetup>().isServer)
            {
                playersGO[i].GetComponent<PlayerSetup>().matchTime = seconds;
                break;
            }
        }

        if (seconds > 0f)
            seconds -= Time.deltaTime;
	}

    public float GetSeconds()
    {
        return seconds;
    }
}
