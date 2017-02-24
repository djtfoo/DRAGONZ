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
    [Client]
	void Start ()
    {
        //if (!owner.GetComponent<Player>().isLocalPlayer)
        //    return;

        //Vel = ( this.gameObject.transform.position-Target).normalized;
        transform.localScale = Scale;

        // Debug.Log(parti)
        //   particleSystem.transform.ro
        ParticleSysInstianted = (ParticleSystem)Instantiate(particleSystem, this.transform.position, this.transform.rotation);
        ParticleSysInstianted.transform.position = this.gameObject.transform.position;
        ParticleSysInstianted.Play();
        combometer = (ComboMeter)FindObjectOfType<ComboMeter>();
        GetComponent<Rigidbody>().AddForce(Target * MovementSpeed);
	}

	// Update is called once per frame
    [Client]
	void Update () 
    {
        if (owner.GetComponent<Player>().isLocalPlayer)
        {
            //Debug.Log("owner: " + owner.name);
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
	}

    [Client]
    void OnCollisionEnter(Collision col)
    {
        //if (!owner.GetComponent<Player>().isLocalPlayer)
        //    return;

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
            Debug.Log("collided " + col.gameObject.name);
            Debug.Log("owner " + owner.gameObject.name);

            CmdHitPlayer();

            if (col.gameObject.GetComponent<Player>().GetIsDead())
            {
            	//col.gameObject.GetComponent<Player>().SetKiller(owner);
                //RpcSetKiller(col.gameObject, owner);
                col.gameObject.GetComponent<Player>().RpcSetKiller(owner);

                owner.GetComponent<Player>().kills++;
            }

            combometer.AddToComboMeter(1);
            //Health health = col.gameObject.GetComponent<Health>();
            //health.currentHealth -= 50;

            col.gameObject.GetComponent<Health>().RpcTakeDamage(50);

            //Debug.Log(health.currentHealth);

            // destroy fireball
            Destroy(this.gameObject);
        }
    }

    [Command]
    void CmdSetKiller(GameObject collided, GameObject owner)
    {
        //collided.GetComponent<Player>().SetKiller(owner);
    }

    [ClientRpc]
    void RpcSetKiller(GameObject collided, GameObject owner)
    {
        //collided.GetComponent<Player>().SetKiller(owner);
    }

    [Command]
    void CmdHitTerrainParticles()
    {
        Destroy(this.gameObject);
       
        ParticleSysInstianted2 = (ParticleSystem)Instantiate(ShockWaveSystem, this.transform.position, ShockWaveSystem.transform.rotation);
        ParticleSysInstianted2.transform.position = this.gameObject.transform.position;
        ParticleSysInstianted2.Play();
        NetworkServer.Spawn(ParticleSysInstianted2.gameObject);
        
        ParticleSysInstianted3 = (ParticleSystem)Instantiate(ExplosionSystem, this.transform.position, ExplosionSystem.transform.rotation);
        ParticleSysInstianted3.transform.position = this.gameObject.transform.position;
        ParticleSysInstianted3.Play();
        NetworkServer.Spawn(ParticleSysInstianted3.gameObject);
    }

    [Command]
    void CmdHitPlayer()
    {
        Destroy(this.gameObject);

        ParticleSysInstianted2 = (ParticleSystem)Instantiate(ShockWaveSystem, this.transform.position, ShockWaveSystem.transform.rotation);
        ParticleSysInstianted2.transform.position = this.gameObject.transform.position;
        ParticleSysInstianted2.Play();
        NetworkServer.Spawn(ParticleSysInstianted2.gameObject);
    }

   // [Command]
    void CmdHitWaterParticles()
    {
        Destroy(this.gameObject);

        ParticleSystem system1 = (ParticleSystem)Instantiate(hitWaterSystem, this.transform.position, hitWaterSystem.transform.rotation);
        system1.transform.position = this.gameObject.transform.position;
        system1.Play();
      //  NetworkServer.Spawn(system1.gameObject);

        ParticleSystem system2 = (ParticleSystem)Instantiate(waterSplashSystem, this.transform.position, waterSplashSystem.transform.rotation);
        system1.transform.position = this.gameObject.transform.position;
        system1.Play();
      //      NetworkServer.Spawn(system1.gameObject);
    }
}
