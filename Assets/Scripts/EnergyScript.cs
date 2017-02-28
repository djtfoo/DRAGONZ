using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EnergyScript : NetworkBehaviour
{
    public float MaxEnergy, AmtEnergyIncrease, EnergyNeededToRun, currentEnergy,rateEnergyCharge;
    public float timer, timeToRecharge, TimeIncreaseEnergy,AmtenergyCharge,MaxCharge,MinimumCharge;
    public bool recharging, readyToUse,ChargedReadyToUse;
    public Image energyBarImage;
    public Image chargeBarImage;
   // public GameObject energyMeter;
   // public EnergyScript energyClass;

    public UnityEvent ReadyToUseEnergy;

    public bool unchargeEnergy = false;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
            return;

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

    // Charging up energy for an attack
    public void ChargeEnergy()
    {
        recharging = false;
#if UNITY_ANDROID
            if (currentEnergy > 0 && AmtenergyCharge < (MaxCharge - EnergyNeededToRun))
            {
                if (AmtenergyCharge == 0)
                {
                    //AmtenergyCharge += EnergyNeededToRun;
                    currentEnergy -= EnergyNeededToRun;
                    ChargedReadyToUse = true;
                }
                currentEnergy -= rateEnergyCharge;
                AmtenergyCharge += rateEnergyCharge;
            }
#else
        if (currentEnergy > 0 && AmtenergyCharge < MaxCharge)
        {
            if (AmtenergyCharge >= MinimumCharge)
                ChargedReadyToUse = true;
            else
                ChargedReadyToUse = false;
            currentEnergy -= rateEnergyCharge;
            AmtenergyCharge += rateEnergyCharge;
        }
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        timer += Time.deltaTime;

#if UNITY_ANDROID
        energyBarImage.fillAmount = (currentEnergy / MaxEnergy);
        chargeBarImage.fillAmount = (AmtenergyCharge / (MaxCharge - EnergyNeededToRun));
#else
        energyBarImage.fillAmount = (currentEnergy / MaxEnergy); //if()
        chargeBarImage.fillAmount = (AmtenergyCharge / MaxCharge);
#endif
        if (Input.GetKeyDown("z"))
        {
            currentEnergy-=10; ;
        }
        if (Input.GetKeyDown("x"))
        {
            currentEnergy+=10;
        }
        if (unchargeEnergy)
        {
            currentEnergy += rateEnergyCharge;
            AmtenergyCharge -= rateEnergyCharge;
            if (AmtenergyCharge < 0)
            {
                currentEnergy += AmtenergyCharge;
                AmtenergyCharge = 0;
                this.recharging = true;
                this.readyToUse = true;
                this.unchargeEnergy = false;
            }
        }
        else if (recharging)
        {
            if (currentEnergy >= EnergyNeededToRun)
            {
                readyToUse = true;
            }
            else
            {
                readyToUse = false;
            }
            //if(AmtenergyCharge>0)
            //{
            //    currentEnergy += rateEnergyCharge;
            //    AmtenergyCharge -= rateEnergyCharge;
            //}
            if (currentEnergy < MaxEnergy)
            {
                if (timer >= TimeIncreaseEnergy)
                {  
                    currentEnergy += AmtEnergyIncrease;
                    if (currentEnergy > MaxEnergy)
                        currentEnergy = MaxEnergy;
                    timer = 0;
                }
            }
            else
                timer = 0;

        }
    }
}
