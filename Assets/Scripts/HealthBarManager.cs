using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]

public class HealthBarManager : MonoBehaviour {
    //List<PlayerHealth> playerList;
    //public GameObject HealthBarObject;
	//// Use this for initialization
    GameObject MainPlayer;
    Camera camera;
    void Start()
    {
        //playerList = GameObject.FindGameObjectsWithTag("RemotePlayer");
        //PlayerHealth temp;
        
    }
	
	// Update is called once per frame
	void Update () {
        if( MainPlayer != GameObject.FindGameObjectWithTag("Player"))
        MainPlayer = GameObject.FindGameObjectWithTag("Player");
        else
        { 
         if(camera !=MainPlayer.GetComponentInChildren<Camera>())
            camera = MainPlayer.GetComponentInChildren<Camera>();
        }
      
        foreach (GameObject playerGO in GameObject.FindGameObjectsWithTag("RemotePlayer"))    
        {
           Health health = playerGO.GetComponent<Health>();
           health.HealthBarAbovePlayer.transform.position = camera.WorldToScreenPoint(playerGO.transform.position + playerGO.transform.up*50);
           health.MaxHealthBarAbovePlayer.transform.position = health.HealthBarAbovePlayer.transform.position;
        }
	}
}
