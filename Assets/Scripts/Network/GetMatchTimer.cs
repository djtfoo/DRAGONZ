using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GetMatchTimer : MonoBehaviour {

    public Text matchTimerText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        //matchTimerText.text = NetworkManager.singleton.GetComponent<MatchTimer>().GetSeconds().ToString();
	}
}
