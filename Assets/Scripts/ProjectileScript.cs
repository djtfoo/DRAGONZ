using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ProjectileScript : NetworkBehaviour 
{
    [SyncVar]
    public Vector3 Vel;

    public Vector3 Scale;

    [SyncVar]
    public Vector3 Target;

    public float MovementSpeed,velocityDegradePercent,DegradeRateTime;
    public bool VeloctiyDegrade;
    public float lifeTime, timer,particleTimer;
    public ParticleSystem particleSystem,ShockWaveSystem,ExplosionSystem, hitWaterSystem, waterSplashSystem;

    float degradeTimer;
    ParticleSystem ParticleSysInstianted, ParticleSysInstianted2, ParticleSysInstianted3;

    float spawnTrailTimer = 0f;
    private ComboMeter combometer;

    public WorldObject burntGO;

    [SyncVar]
    public GameObject owner;

	// Use this for initialization
	void Start ()
    {
        //Vel = ( this.gameObject.transform.position-Target).normalized;
        transform.localScale = Scale;

        // Debug.Log(parti)
        //   particleSystem.transform.ro
        ParticleSysInstianted = (ParticleSystem)Instantiate(particleSystem, this.transform.position, this.transform.rotation);
        ParticleSysInstianted.transform.position = this.gameObject.transform.position;
        ParticleSysInstianted.Play();
        combometer = owner.GetComponent<ComboMeter>(); //(ComboMeter)FindObjectOfType<ComboMeter>();
        GetComponent<Rigidbody>().AddForce(Target * MovementSpeed);
	}

	// Update is called once per frame
	void Update () 
    {
        //Debug.DrawLine(this.transform.position, this.transform.position + GetComponent<Rigidbody>().GetPointVelocity(this.transform.position).normalized * 1000, Color.red);
        timer += Time.deltaTime;

        if (VeloctiyDegrade)
        {
            degradeTimer += Time.deltaTime;
            if (degradeTimer >= DegradeRateTime)
            {
                //  Vel *= ((100-velocityDegradePercent) / 100);
                GetComponent<Rigidbody>().AddForce(-((Target) * ((100 - velocityDegradePercent) / 100)));
                degradeTimer = 0;
            }
        }

        spawnTrailTimer += Time.deltaTime;
        if (spawnTrailTimer > 0.5f)
        {
            spawnTrailTimer = 0f;
            ParticleSystem instantiateTrail = (ParticleSystem)Instantiate(particleSystem, this.transform.position, this.transform.rotation);

            instantiateTrail.gameObject.transform.Rotate(new Vector3(-Vel.x, 180f, -Vel.z));
            instantiateTrail.gameObject.transform.position = this.transform.position;

            instantiateTrail.Play();
        }

        //ParticleSystem instantiateTrail = (ParticleSystem)Instantiate(particleSystem, this.transform.position, this.transform.rotation);
        //
        //instantiateTrail.gameObject.transform.Rotate(new Vector3(-Vel.x, 180f, -Vel.z));
        //  this.gameObject.transform.position += Vel * Time.deltaTime * MovementSpeed;
        //instantiateTrail.gameObject.transform.position = this.transform.position;
        //Debug.Log(this.ParticleSysInstianted.gameObject.transform.eulerAngles);
        if (timer > lifeTime)
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);

        }
	}

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Terrain")
        {
            //Debug.Log(GetTerrainHeight.GetHeight(col.gameObject, this.transform.position));

            if (GetTerrainHeight.GetHeight(col.gameObject, this.transform.position) > MapData.regions[0].height)
            {
                // hit ground
                CmdHitTerrainParticles();
                combometer.AddToComboMeter(1);
                //BurntTexture burntTexture = GameObject.FindObjectOfType<BurntTexture>();
               // burntTexture.InstantiateBurntTexture(col.contacts[0].point + (col.contacts[0].normal*5f), Quaternion.FromToRotation(Vector3.up, col.contacts[0].normal));
                CreateBurntTexture.InstantiateBurntTexture(burntGO, col.contacts[0].point + (col.contacts[0].normal * 8f), Quaternion.FromToRotation(Vector3.up, col.contacts[0].normal));
                //if (burngm)
                //{
                //    burngm.transform.Rotate(Vector3.left, 70);
                //}
            }
            else
            {
                // hit water
                CmdHitWaterParticles();
            }
        }

        if (col.gameObject.tag == "WorldObject")
        {
            CmdHitTerrainParticles();
            combometer.AddToComboMeter(1);
        }

        // Collides with remote players
        if (col.gameObject.tag == "Player" && col.gameObject.name != owner.name)
        {
            CmdHitPlayer();
            CmdTakeDamage(col.gameObject.GetComponent<Player>().netId, 40);
            CmdSetKillerName(col.gameObject.GetComponent<Player>().netId, owner.gameObject.GetComponent<Player>().netId);

            combometer.AddToComboMeter(1);
        }
    }

    [Command]
    void CmdTakeDamage(NetworkInstanceId _netID, int _damage)
    {
        NetworkServer.FindLocalObject(_netID).GetComponent<Health>().TakeDamage(_damage);
    }

    [Command]
    void CmdSetKillerName(NetworkInstanceId _collidedID, NetworkInstanceId _killerID)
    {
        if (NetworkServer.FindLocalObject(_collidedID).GetComponent<Player>().GetDeadStatus())
        {
            string killerName = NetworkServer.FindLocalObject(_killerID).GetComponent<Player>().username;
            NetworkServer.FindLocalObject(_collidedID).GetComponent<Player>().RpcSetKillerName(killerName);
            NetworkServer.FindLocalObject(_collidedID).GetComponent<Player>().RpcIncreaseStatsCount("Deaths");
            NetworkServer.FindLocalObject(_killerID).GetComponent<Player>().RpcIncreaseStatsCount("Kills");
        }
    }

    [Command]
    void CmdHitTerrainParticles()
    {
        AudioManager.instance.PlayFireballHitGroundSFX();
#if UNITY_ANDROID
        //if (SettingsData.IsVibrationOn())
        //    Handheld.Vibrate();
#endif
        ParticleSysInstianted2 = (ParticleSystem)Instantiate(ShockWaveSystem, this.transform.position, ShockWaveSystem.transform.rotation);
        ParticleSysInstianted2.transform.position = this.gameObject.transform.position;
        ParticleSysInstianted2.Play();
        NetworkServer.Spawn(ParticleSysInstianted2.gameObject);
        
        ParticleSysInstianted3 = (ParticleSystem)Instantiate(ExplosionSystem, this.transform.position, ExplosionSystem.transform.rotation);
        ParticleSysInstianted3.transform.position = this.gameObject.transform.position;
        ParticleSysInstianted3.Play();
        NetworkServer.Spawn(ParticleSysInstianted3.gameObject);

        Destroy(this.gameObject);
    }

    [Command]
    void CmdHitPlayer()
    {
#if UNITY_ANDROID
        //if (SettingsData.IsVibrationOn())
        //    Handheld.Vibrate();
#endif
        ParticleSysInstianted2 = (ParticleSystem)Instantiate(ShockWaveSystem, this.transform.position, ShockWaveSystem.transform.rotation);
        ParticleSysInstianted2.transform.position = this.gameObject.transform.position;
        ParticleSysInstianted2.Play();
        NetworkServer.Spawn(ParticleSysInstianted2.gameObject);

        Destroy(this.gameObject);
    }

    [Command]
    void CmdHitWaterParticles()
    {
        AudioManager.instance.PlayWaterSplashSFX();
#if UNITY_ANDROID
        //if (SettingsData.IsVibrationOn())
        //    Handheld.Vibrate();
#endif
        ParticleSysInstianted2 = (ParticleSystem)Instantiate(hitWaterSystem, this.transform.position, hitWaterSystem.transform.rotation);
        ParticleSysInstianted2.transform.position = this.gameObject.transform.position;
        ParticleSysInstianted2.Play();
        NetworkServer.Spawn(ParticleSysInstianted2.gameObject);

        ParticleSysInstianted3 = (ParticleSystem)Instantiate(waterSplashSystem, this.transform.position, waterSplashSystem.transform.rotation);
        ParticleSysInstianted3.transform.position = this.gameObject.transform.position;
        ParticleSysInstianted3.Play();
        NetworkServer.Spawn(ParticleSysInstianted3.gameObject);

        Destroy(this.gameObject);
    }
}
