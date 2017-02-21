using UnityEngine;
using System.Collections;

public class Temperature : MonoBehaviour {
    public float currentTemperature,MaxTemperature,FireLifeTime,timer,MinTempExtinguish,RValue;
    public bool onFIRE,Burnt;


    public ParticleSystem onFireParticleSystem;
    //ParticleSystem onFireActivated;
    public HeatTransfer heatTransfer;
	// Use this for initialization
	void Start () {
        onFIRE = false;
        Burnt = false;
        heatTransfer = FindObjectOfType<HeatTransfer>();
        heatTransfer.AddToGoList(this.gameObject);
        onFireParticleSystem = (ParticleSystem)Instantiate(onFireParticleSystem, this.gameObject.transform.position, this.gameObject.transform.rotation);
        onFireParticleSystem.transform.position = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (onFIRE)
        {
            onFireParticleSystem.gameObject.SetActive(true);
        }
        if (!onFIRE)
        {
            onFireParticleSystem.gameObject.SetActive(false);
        }
        if (currentTemperature >= MaxTemperature && !Burnt)
        {
            onFIRE = true;
        }
        if (onFIRE && !Burnt)
        {
            timer += Time.deltaTime;
            if (timer >= FireLifeTime)
            {
                Burnt = true;
                onFIRE = false;
                timer = 0;
            }
            if (currentTemperature <= MinTempExtinguish)
            {
                onFIRE = false;
                timer = 0;
            }
        }
        if (Burnt)
        {
            heatTransfer.DeleteToGoList(this.gameObject);
            Destroy(onFireParticleSystem.gameObject);
            Destroy(this.gameObject);
        }
            //if(Burnt) //Test Case
            //{
            //     timer += Time.deltaTime;
            //     if(timer>=5)
            //     {
            //      onFIRE = true;
            //      Burnt = false;
            //      timer = 0;
            //     }
            
            //}
    
            
	}
}
