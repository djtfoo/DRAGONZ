using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DragonAttack : NetworkBehaviour
{

    public EnergyScript energy;
    public GameObject Projectile, Target;

    public GameObject dragonMouth;

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
        if (!isLocalPlayer)
            return;

        player = GetComponent<Player>();//GameObject.GetComponent<Player>();
        keypress = false;
        test = 0;
        exceptionCharge = false;
    }

    // Update is called once per frame
    [Client]
    void Update()
    {
        if (!isLocalPlayer || OverlayActive.IsOverlayActive())
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
            CmdChargedAttack();
            exceptionCharge = false;
        }
    }

    [Command]
    public void CmdChargedAttack()
    {
        GameObject ProjectileInstianted = (GameObject)Instantiate(Projectile, this.transform.position, Quaternion.identity);
        ProjectileInstianted.GetComponent<ProjectileScript>().MovementSpeed += energy.AmtenergyCharge * 50;
        //debug.text = ProjectileInstianted.GetComponent<ProjectileScript>().MovementSpeed.ToString();
        energy.AmtenergyCharge = 0;
        energy.recharging = true;
        energy.timer = 0;
        energy.ChargedReadyToUse = false;

        ProjectileScript projScript = ProjectileInstianted.GetComponent<ProjectileScript>();
        player = GetComponent<Player>();
        projScript.owner = player; // this.gameObject; // player.gameObject;

        //projScript.Target = (Target.transform.position - this.transform.position).normalized;
        //projScript.gameObject.transform.position = this.transform.position;

        projScript.Target = (Target.transform.position - dragonMouth.transform.position).normalized;
        projScript.gameObject.transform.position = dragonMouth.transform.position;

        projScript.Vel = projScript.Target;

        NetworkServer.Spawn(ProjectileInstianted);
        test++;
    }

    [Command]
    public void CmdFireBallAttack()
    {
        player = GetComponent<Player>();
        //Debug.Log(player.name + " view: " + player.GetView()); // view is always (0, 0, 1) for 2nd player

        //debug.text = "Fired";
        GameObject ProjectileInstianted = (GameObject)Instantiate(Projectile, this.transform.position, Quaternion.identity);

        energy.DecreaseEnergy();
        ProjectileScript projScript = ProjectileInstianted.GetComponent<ProjectileScript>();
        projScript.owner = player; // this.gameObject; //player.gameObject;

        //projScript.Target = player.GetView();
        //projScript.Target = (Target.transform.position - this.transform.position).normalized;
        //projScript.gameObject.transform.position = this.transform.position;
        projScript.Target = (Target.transform.position - dragonMouth.transform.position).normalized;
        projScript.gameObject.transform.position = dragonMouth.transform.position;

        projScript.Vel = projScript.Target;

        NetworkServer.Spawn(ProjectileInstianted);
        test++;
    }
}
