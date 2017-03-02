using UnityEngine;
using System.Collections;

public class Temperature : MonoBehaviour {
    public float currentTemperature,MaxTemperature,FireLifeTime,timer,MinTempExtinguish,RValue;
    public bool onFIRE,Burnt;


    public ParticleSystem onFireParticleSystem;
    ParticleSystem InstantiantedParticleSystem;
    //ParticleSystem onFireActivated;
    public HeatTransfer heatTransfer;
	// Use this for initialization
	void Start () {
        onFIRE = false;
        Burnt = false;
        heatTransfer = FindObjectOfType<HeatTransfer>();
        heatTransfer.AddToGoList(this.gameObject);
           // onFireParticleSystem.gameObject.SetActive(false);
        InstantiantedParticleSystem = (ParticleSystem)Instantiate(onFireParticleSystem, this.gameObject.transform.position+this.gameObject.transform.up*10, onFireParticleSystem.transform.rotation);
        InstantiantedParticleSystem.transform.position = this.gameObject.transform.position + this.gameObject.transform.up * 10;
        InstantiantedParticleSystem.GetComponent<ParticleSystemUpdate>().enabled = false;
        InstantiantedParticleSystem.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if(currentTemperature<=0)
        {
            currentTemperature = 0;
        }
        if (onFIRE)
        {
            InstantiantedParticleSystem.gameObject.SetActive(true);
        }
        if (!onFIRE)
        {
            InstantiantedParticleSystem.gameObject.SetActive(false);
        }
        if (currentTemperature >= MaxTemperature && !Burnt)
        {
            onFIRE = true;
        }
        if (onFIRE && !Burnt)
        {
            FireLifeTime -= Time.deltaTime;
            currentTemperature = MaxTemperature;
            if (FireLifeTime <=0)
            {
                Burnt = true;
                onFIRE = false;
                timer = 0;
            }
        }
        if (Burnt)
        {
            heatTransfer.DeleteToGoList(this.gameObject);
            Destroy(InstantiantedParticleSystem.gameObject);
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
