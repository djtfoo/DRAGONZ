using UnityEngine;
using System.Collections;

public class Destruction : MonoBehaviour {

    public Transform BrokenBall;
    public Transform effect;

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "WorldObject")
        {
            BreakDeath();
        }
    }

    void BreakDeath()
    {
        Debug.Log("BreakDeathRead");
        Instantiate(BrokenBall, transform.position, BrokenBall.transform.rotation);
        // instantiate particle effect here
        // play sound
        Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
