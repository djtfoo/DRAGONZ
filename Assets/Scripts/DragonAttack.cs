using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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

    private GameObject ProjectileInstianted;

    // Use this for initialization
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
    void Update()
    {
        // Checks for overlay active done in Player.cs
        if (!isLocalPlayer)
            return;

        if (energyMeterText != null)
            energyMeterText.text = energy.currentEnergy.ToString();

        if (Input.GetMouseButtonUp(0) && energy.readyToUse)
        {
            CmdFireBallAttack(FireBallTarget());
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
            CmdChargedAttack(FireBallTarget());
            exceptionCharge = false;
        }
    }

    [Command]
    public void CmdChargedAttack(Vector3 _target)
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
        //projScript.owner = player; // this.gameObject; // player.gameObject;
        projScript.owner = this.gameObject;

        //projScript.Target = (Target.transform.position - this.transform.position).normalized;
        //projScript.gameObject.transform.position = this.transform.position;

        projScript.Target = _target;
        projScript.gameObject.transform.position = dragonMouth.transform.position;

        projScript.Vel = projScript.Target;

        NetworkServer.Spawn(ProjectileInstianted);
        test++;
    }

    [Command]
    void CmdFireBallAttack(Vector3 _target)
    {
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
