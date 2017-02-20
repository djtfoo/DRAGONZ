using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DragonAttack : NetworkBehaviour
{

    public EnergyScript energy;
    public GameObject DragonMouth, Projectile, Target;
    //public Text debug;
    public Text energyMeterText;
    public bool exceptionCharge;
    bool keypress;
    int test;
    Player player;

    // Use this for initialization
    [Client]
    void Start() // public override void OnStartLocalPlayer()
    {
        player = GetComponent<Player>();//GameObject.GetComponent<Player>();
        keypress = false;
        test = 0;
        exceptionCharge = false;
    }

    // Update is called once per frame
    [Client]
    void Update()
    {
        if (!player.isLocalPlayer || PauseMenu.isOn)
            return;

        if (energyMeterText != null)
            energyMeterText.text = energy.currentEnergy.ToString();

        if (Input.GetMouseButtonUp(0) && energy.readyToUse)
        {
            CmdFireBallAttack();
        }
        else
            keypress = false;

        if (energy.currentEnergy >= energy.MinimumCharge)
        {
            exceptionCharge = true;
        }
           
        if (Input.GetMouseButton(1) && (energy.currentEnergy >= energy.MinimumCharge || exceptionCharge))
        {
            energy.ChargeEnergy();
        }
        if (Input.GetMouseButtonUp(1) && energy.ChargedReadyToUse)
        {
            ChargedAttack();
            exceptionCharge = false;
        }
    }

    public void ChargedAttack()
    {
        GameObject ProjectileInstianted = (GameObject)Instantiate(Projectile, DragonMouth.transform.position, DragonMouth.transform.rotation);
        ProjectileInstianted.GetComponent<ProjectileScript>().MovementSpeed += energy.AmtenergyCharge * 10;
        //debug.text = ProjectileInstianted.GetComponent<ProjectileScript>().MovementSpeed.ToString();
        energy.AmtenergyCharge = 0;
        energy.recharging = true;
        energy.timer = 0;
        energy.ChargedReadyToUse = false;

        ProjectileScript script = ProjectileInstianted.GetComponent<ProjectileScript>();
        Player player = GetComponent<Player>();
        script.Target = player.GetView();
        script.gameObject.transform.position = this.transform.position;

        NetworkServer.Spawn(ProjectileInstianted);

        script.Vel = script.Target;
        test++;
    }

    [Command]
    public void CmdFireBallAttack()
    {
        Debug.Log(player.name + " view: " + player.GetView()); // view is always (0, 0, 1) for 2nd player

        //debug.text = "Fired";
        GameObject ProjectileInstianted = (GameObject)Instantiate(Projectile, DragonMouth.transform.position, DragonMouth.transform.rotation);

        energy.DecreaseEnergy();
        ProjectileScript projScript = ProjectileInstianted.GetComponent<ProjectileScript>();

        projScript.Target = player.GetView();
        projScript.gameObject.transform.position = this.transform.position;
        projScript.Vel = projScript.Target;

        NetworkServer.Spawn(ProjectileInstianted);
        test++;
    }
}
