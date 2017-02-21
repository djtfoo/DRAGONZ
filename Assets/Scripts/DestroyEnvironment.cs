using UnityEngine;
using System.Collections;

public class DestroyEnvironment : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "TestCube")
        {
            Radar.RemoveRadarObject(col.gameObject);
            //Destroy(col.gameObject);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
