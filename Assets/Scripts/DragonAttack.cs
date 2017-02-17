using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DragonAttack : MonoBehaviour {
    public EnergyScript energy;
    public GameObject DragonMouth,Projectile,Target;
    //public Text debug;
    public Text energyMeterText;
    public bool exceptionCharge;
    bool keypress;
    int test;

	// Use this for initialization
	void Start () {
        keypress = false;
        test=0;
        exceptionCharge = false;
	}
	
	// Update is called once per frame
	void Update () {
        energyMeterText.text = energy.currentEnergy.ToString();
        if (Input.GetMouseButtonUp(0) && energy.readyToUse)
        {
            FireBallAttack();
        }
        else
            keypress = false;
        if(energy.currentEnergy>=energy.MinimumCharge)
        {
            exceptionCharge=true;
        }
        if((Input.GetMouseButton(1)&& (energy.currentEnergy>energy.MinimumCharge || exceptionCharge)))
        {
            energy.ChargeEnergy();
        }
        if(Input.GetMouseButtonUp(1)&&energy.ChargedReadyToUse)
        {
            ChargedAttack();
             exceptionCharge=false;
        }
	}
    public void ChargedAttack()
    {
        GameObject ProjectileInstianted = (GameObject)Instantiate(Projectile, DragonMouth.transform.position, DragonMouth.transform.rotation);
         ProjectileInstianted.GetComponent<ProjectileScript>().MovementSpeed+= energy.AmtenergyCharge*10;
         //debug.text = ProjectileInstianted.GetComponent<ProjectileScript>().MovementSpeed.ToString();
        energy.AmtenergyCharge = 0;
        energy.recharging = true;
        energy.timer = 0;
        energy.ChargedReadyToUse = false;
       
        ProjectileScript script = ProjectileInstianted.GetComponent<ProjectileScript>();
        Player player = GameObject.FindObjectOfType<Player>();
        script.Target = player.GetView();
        script.gameObject.transform.position = this.transform.position;

        script.Vel = script.Target;
        test++;
        
    }
    public void FireBallAttack()
    {
        //debug.text = "Fired";
       GameObject ProjectileInstianted = (GameObject)Instantiate(Projectile, DragonMouth.transform.position, DragonMouth.transform.rotation);
       energy.DecreaseEnergy(  );
       ProjectileScript script2 = ProjectileInstianted.GetComponent<ProjectileScript>();

       Player player = GameObject.FindObjectOfType<Player>();
       script2.Target = player.GetView();
       script2.gameObject.transform.position = this.transform.position;

       script2.Vel = script2.Target;
       test++;
    }
}
