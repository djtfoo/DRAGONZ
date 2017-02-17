using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
public class EnergyScript : MonoBehaviour
{
    public float MaxEnergy, AmtEnergyIncrease, EnergyNeededToRun, currentEnergy,rateEnergyCharge;
    public float timer, timeToRecharge, TimeIncreaseEnergy,AmtenergyCharge,MaxCharge,MinimumCharge;
    public bool recharging, readyToUse,ChargedReadyToUse;
    public Image energyBarImage;
    public Image chargeBarImage;
   // public GameObject energyMeter;
   // public EnergyScript energyClass;

    public UnityEvent ReadyToUseEnergy;

    // Use this for initialization
    void Start()
    {
        energyBarImage.fillAmount = 0.9f;
        //  entry.eventID = energyClass.onStoreEnergyFull();
        // entry.callback.AddListener( (eventData) => { } );

        //  MaxEnergy = 0;
        //  rateOfEnergyDecrease = 0;
        //// rateOfEnergyIncrease = 0;
        //currentEnergy = 0;
        // EnergyNeededToRun = 0;
        //recharging = false;
        //TimeIncreaseEnergy = 0;

    }
    public void EnergyReadyToUse()
    {
    }
    public void DecreaseEnergy()
    { 
        currentEnergy -= EnergyNeededToRun;
    }
    // Update is called once per frame
    public void ChargeEnergy()
    {
        recharging = false;
        if (currentEnergy > 0 && AmtenergyCharge <MaxCharge)
        {
            ChargedReadyToUse = true;
            currentEnergy -= rateEnergyCharge;
            AmtenergyCharge += rateEnergyCharge;
        }

    }
    void Update()
    {
        timer += Time.deltaTime;

        energyBarImage.fillAmount = (currentEnergy / MaxEnergy); //if()
        chargeBarImage.fillAmount = (AmtenergyCharge / MaxCharge);
        if (Input.GetKeyDown("z"))
        {
            currentEnergy-=10; ;
        }
        if (Input.GetKeyDown("x"))
        {
            currentEnergy+=10;
        }
        if (recharging)
        {
            if (currentEnergy >= EnergyNeededToRun)
            {
                readyToUse = true;
            }
            else
            {
                readyToUse = false;
            }
            if(AmtenergyCharge>0)
            {
                currentEnergy += rateEnergyCharge;
                AmtenergyCharge -= rateEnergyCharge;
            }
            if (currentEnergy < MaxEnergy)
            {
                if (timer >= TimeIncreaseEnergy)
                {  
                    if (currentEnergy >= MaxEnergy)
                        currentEnergy = MaxEnergy;
                    else
                    currentEnergy += AmtEnergyIncrease;
                  
                    timer = 0;
                }
            }
            else
                timer = 0;

        }
    }
}
