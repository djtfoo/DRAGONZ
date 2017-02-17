using UnityEngine;
using UnityEngine.Networking;

public class ProjectileScript : NetworkBehaviour {
    public Vector3 Vel, Scale,Target;
    public float MovementSpeed,velocityDegradePercent,DegradeRateTime;
    public bool VeloctiyDegrade;
    public float lifeTime, timer,particleTimer;
   public ParticleSystem particleSystem,ShockWaveSystem,ExplosionSystem;
   float degradeTimer;
    ParticleSystem ParticleSysInstianted, ParticleSysInstianted2, ParticleSysInstianted3;
      


	// Use this for initialization
	void Start ()
    {
        //Vel = ( this.gameObject.transform.position-Target).normalized;
        transform.localScale = Scale;
	}

	// Update is called once per frame
    [Client]
	void Update () {
        timer += Time.deltaTime;
        if (VeloctiyDegrade)
        {
            degradeTimer += Time.deltaTime;
            if (degradeTimer >= DegradeRateTime)
            {
                Vel *= ((100-velocityDegradePercent) / 100);
                degradeTimer = 0;
            }
        }
        
        this.gameObject.transform.position += Vel * Time.deltaTime * MovementSpeed;
        if(timer > lifeTime)
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);

        }
    
	}
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag=="Terrain")
        {
            Destroy(this.gameObject);
            ParticleSysInstianted = (ParticleSystem)Instantiate(particleSystem, this.transform.position, particleSystem.transform.rotation);
            ParticleSysInstianted.transform.position = this.gameObject.transform.position;
            ParticleSysInstianted.Play();

            ParticleSysInstianted2 = (ParticleSystem)Instantiate(ShockWaveSystem, this.transform.position, ShockWaveSystem.transform.rotation);
            ParticleSysInstianted2.transform.position = this.gameObject.transform.position;
            ParticleSysInstianted2.Play();

            ParticleSysInstianted3 = (ParticleSystem)Instantiate(ExplosionSystem, this.transform.position, ExplosionSystem.transform.rotation);
            ParticleSysInstianted3.transform.position = this.gameObject.transform.position;

            ParticleSysInstianted3.Play();
        }
    }
}
