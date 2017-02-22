using UnityEngine;
using System.Collections;
using System.Net;
using UnityEngine.Networking;
public class Chat : MonoBehaviour {
    
    private NetworkManager networkManager;
    private const short chatHandler = 1300;
	// Use this for initialization
	void Start () {
        networkManager = NetworkManager.singleton;

	}
	
	// Update is called once per frame
	void Update () {
                	
	}
}
