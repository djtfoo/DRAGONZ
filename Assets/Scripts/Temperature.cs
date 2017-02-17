using UnityEngine;
using System.Collections;

public class Temperature : MonoBehaviour {
    public float currentTemperature,MaxTemperature,FireLifeTime,timer,MinTempExtinguish;
    public bool onFIRE,Burnt;
    public HeatTransfer heatTransfer;
	// Use this for initialization
	void Start () {
        onFIRE = false;
        Burnt = false;
	}
	
	// Update is called once per frame
	void Update () {
	        if(currentTemperature >=MaxTemperature)
            {
                onFIRE = true;
            }
            if(onFIRE&&!Burnt)
            {
                timer += Time.deltaTime;
                if(timer>=FireLifeTime)
                {
                    Burnt = true;
                    timer = 0;
                }
                if (currentTemperature <= MinTempExtinguish)
                {
                    onFIRE = false;
                    timer = 0;
                }
            }
	}
}
