using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DragonAttack : NetworkBehaviour
{

    public EnergyScript energy;
    public GameObject Projectile, Target;

    [SyncVar]
    public GameObject dragonMouth;

    //public Text debug;
    public Text energyMeterText;
    public bool exceptionCharge;
    
    bool keypress;
    int test;
    Player player;
    Light lightsource;
    
   // public Color ColorRGB;
    private GameObject ProjectileInstianted;

    public GraphicRaycaster raycaster;
    public float LightSourceIntensityMultilpier;
    public float MaxLightIntensity;
    // for shooting in Android
    private int touchID = -1;
    
    // Use this for initialization
    void Start() // public override void OnStartLocalPlayer()
    {
        if (!isLocalPlayer)
            return;

        player = GetComponent<Player>();//GameObject.GetComponent<Player>();
        keypress = false;
        test = 0;
        exceptionCharge = false;
        lightsource = gameObject.transform.GetChild(1).GetChild(0).GetComponent<Light>();   // mouth's child is Light
    }

    // Update is called once per frame
    void Update()
    {
        // Checks for overlay active done in Player.cs
        if (!isLocalPlayer)
            return;
#if UNITY_ANDROID
        if (touchID == -1)  // not touching the screen; not charging up yet
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)    // if the touch just began, check if it's not on a GUI object
                {
                    PointerEventData ped = new PointerEventData(null);
                    ped.position = Input.GetTouch(i).position;
                    List<RaycastResult> results = new List<RaycastResult>();
                    raycaster.Raycast(ped, results);
                    if (results.Count == 0)
                    {
                        if (energy.currentEnergy >= energy.EnergyNeededToRun) {
                            exceptionCharge = true;
                            touchID = i;
                            break;
                        }
                    }
                }
            }
        }
        else
        {   // there is a touch; start to charge up
            if (Input.GetTouch(touchID).phase == TouchPhase.Ended)    // if the touch has ended, release the charged fireball
            {
                CmdChargedAttack(FireBallTarget());
                exceptionCharge = false;
                touchID = -1;
            }
            else if (energy.currentEnergy >= energy.MinimumCharge || exceptionCharge)
            {
                energy.ChargeEnergy();
            }
        }
#else
        if (energyMeterText != null)
            energyMeterText.text = energy.currentEnergy.ToString();

        if (Input.GetKeyDown(KeyBoardBindings.GetAttackKey()) && energy.readyToUse)
        {
            CmdFireBallAttack(FireBallTarget());
        }
        else
            keypress = false;

        if (energy.currentEnergy >= energy.MinimumCharge)
        {
            exceptionCharge = true;
        }
           
        if (Input.GetKey(KeyBoardBindings.GetChargedAttackKey()) && (energy.currentEnergy >= energy.MinimumCharge || exceptionCharge))
        {
            energy.ChargeEnergy();
            if (lightsource.intensity <= MaxLightIntensity)
                lightsource.intensity = (energy.AmtenergyCharge / energy.MaxCharge) * MaxLightIntensity;


    //material.SetColor("_Color",Color.Lerp(Color.white, new Color(1f,0.5f,0f), energy.AmtenergyCharge / energy.MaxCharge));
        }
        else
        if (lightsource.intensity> 0)
                lightsource.intensity -= Time.deltaTime * LightSourceIntensityMultilpier;
        if (Input.GetKeyUp(KeyBoardBindings.GetChargedAttackKey()))
        {
#if UNITY_ANDROID
            CmdChargedAttack(FireBallTarget());
            exceptionCharge = false;
#else
            

            if (energy.ChargedReadyToUse)
            {
                CmdChargedAttack(FireBallTarget());
                exceptionCharge = false;    
            }
            else
            {
                //energy.recharging = true;
                energy.unchargeEnergy = true;
                energy.readyToUse = false;
                exceptionCharge = false;
            }
#endif
        }

        //if (unchargeEnergy)
        //{
        //    unchargeEnergy = energy.UnchargeEnergy();
        //}
#endif
    }

    [Command]
    public void CmdChargedAttack(Vector3 _target)
    {
        AudioManager.instance.PlayChargedFireballReleaseSFX();
#if UNITY_ANDROID
        if (SettingsData.IsVibrationOn())
            Handheld.Vibrate();
#endif

        GameObject ProjectileInstianted = (GameObject)Instantiate(Projectile, this.transform.position, Quaternion.identity);
        ProjectileInstianted.GetComponent<ProjectileScript>().MovementSpeed += energy.AmtenergyCharge * 50;
        //debug.text = ProjectileInstianted.GetComponent<ProjectileScript>().MovementSpeed.ToString();
        energy.AmtenergyCharge = 0;
        energy.recharging = true;
        energy.timer = 0;
        energy.ChargedReadyToUse = false;

        ProjectileScript projScript = ProjectileInstianted.GetComponent<ProjectileScript>();
        player = GetComponent<Player>();
        //projScript.owner = player; // this.gameObject; // player.gameObject;
        projScript.owner = this.gameObject;

        //projScript.Target = (Target.transform.position - this.transform.position).normalized;
        //projScript.gameObject.transform.position = this.transform.position;

        projScript.Target = _target;
        projScript.gameObject.transform.position = dragonMouth.transform.position;

        projScript.Vel = projScript.Target;

        NetworkServer.SpawnWithClientAuthority(ProjectileInstianted, connectionToClient);
        test++;
    }

    [Command]
    void CmdFireBallAttack(Vector3 _target)
    {
        AudioManager.instance.PlayFireballReleaseSFX();
#if UNITY_ANDROID
        if (SettingsData.IsVibrationOn())
            Handheld.Vibrate();
#endif

        player = GetComponent<Player>();

        ProjectileInstianted = (GameObject)Instantiate(Projectile, this.transform.position, Quaternion.identity);

        energy.DecreaseEnergy();

        ProjectileScript projScript = ProjectileInstianted.GetComponent<ProjectileScript>();
        projScript.owner = this.gameObject;
        projScript.Target = _target;
        projScript.gameObject.transform.position = dragonMouth.transform.position;
        projScript.Vel = projScript.Target;

        NetworkServer.SpawnWithClientAuthority(ProjectileInstianted, connectionToClient);
    }

    Vector3 FireBallTarget()
    {
        return (Target.transform.position - dragonMouth.transform.position).normalized;
    }
}
