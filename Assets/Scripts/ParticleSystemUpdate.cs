using UnityEngine;
using System.Collections;

public class ParticleSystemUpdate : MonoBehaviour {

    public ParticleSystem particleSystem;
    public bool enabled;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (enabled)
        {
            if (!particleSystem.IsAlive())
            {
                Destroy(this.gameObject);
            }
        }
            //particleSystem.emissionRate=
	}
}
