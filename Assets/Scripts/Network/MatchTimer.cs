using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MatchTimer : NetworkBehaviour {

    public int minutes;

    private float seconds;  // the set time converted to seconds
    private GameObject theHost;
    private GameObject[] playersGO;

	// Use this for initialization
	void Start () 
    {
        playersGO = GameObject.FindGameObjectsWithTag("Player");
        if (playersGO[0].GetComponent<PlayerSetup>().isServer)
            theHost = playersGO[0];
        else
            return;

        seconds = (float)(minutes * 60);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (theHost == null)
            return;

        theHost.GetComponent<PlayerSetup>().matchTime = seconds;

        playersGO = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < playersGO.Length; ++i)
        {
            if (playersGO[i].layer == 9)//(!playersGO[i].GetComponent<PlayerSetup>().isServer)
            {
                if (playersGO[i] != null && playersGO[i] != theHost)
                {
                    playersGO[i].GetComponent<PlayerSetup>().RpcSetMatchTime(seconds);
                }
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
