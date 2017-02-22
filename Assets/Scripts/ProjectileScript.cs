using UnityEngine;
using UnityEngine.Networking;

public class ProjectileScript : NetworkBehaviour 
{
    public Vector3 Vel, Scale,Target;
    public float MovementSpeed,velocityDegradePercent,DegradeRateTime;
    public bool VeloctiyDegrade;
    public float lifeTime, timer,particleTimer;
    public ParticleSystem particleSystem,ShockWaveSystem,ExplosionSystem;
    float degradeTimer;
    ParticleSystem ParticleSysInstianted, ParticleSysInstianted2, ParticleSysInstianted3;

    float spawnTrailTimer = 0f;
    private ComboMeter combometer;
    public Player owner;

	// Use this for initialization
    [Client]
	void Start ()
    {
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
        //Debug.Log("owner: " + owner.name);

        timer += Time.deltaTime;
        if (VeloctiyDegrade)
        {
            degradeTimer += Time.deltaTime;
            if (degradeTimer >= DegradeRateTime)
            {
              //  Vel *= ((100-velocityDegradePercent) / 100);
                  GetComponent<Rigidbody>().AddForce(-((Target)* ((100-velocityDegradePercent) / 100)));
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
        if(timer > lifeTime)
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);

        }
	}

    [Client]
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Terrain")
        {
            CmdHitTerrainParticles();
            combometer.AddToComboMeter(1);
        }

        if (col.gameObject.tag == "WorldObject")
        {
            CmdHitTerrainParticles();
            combometer.AddToComboMeter(1);
        }

        // Collides with remote players
        if (col.gameObject.tag == "Player" && col.gameObject.name != owner.gameObject.name)
        {
            CmdHitPlayer();

            combometer.AddToComboMeter(1);
            Health health = col.gameObject.GetComponent<Health>();
            health.currentHealth -= 10;
            //Debug.Log(health.currentHealth);
        }
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
}
