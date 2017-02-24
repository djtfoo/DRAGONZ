using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour
{
    [SyncVar]
    private bool isDead = false;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    public float timeTillRespawn;
    private float respawnTimer;

    public int kills;
    public int deaths;
    private GameObject killer;

    public void SetKiller(GameObject _killer)
    {
        killer = _killer;
    }

    public void Setup()
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }

        SetDefaults();
    }

    public void SetDefaults()
    {
        respawnTimer = timeTillRespawn;
        killer = null;
        isDead = false;

        GetComponent<PlayerMovement>().SetDefault();
        GetComponent<Health>().SetDefault();

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = true;
    }

    public void SetIsDead(bool _isDead)
    {
        isDead = _isDead;
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    // Use this for initialization
    public override void OnStartLocalPlayer()
    {
    }

    // Update is called once per frame
    [Client]
    void Update()
    {
        if (isLocalPlayer)
        {
            if (OverlayActive.IsOverlayActive())
                return;

            if (GetComponent<Health>().currentHealth <= 0.0f)
            {
                Die();
                SetKiller(gameObject);
            }

            if (Input.GetKeyDown(KeyCode.K))
                GetComponent<Health>().currentHealth = 0;

            if (killer != null)
            {
                GetComponent<PlayerSetup>().GetPlayerUI().GetRespawnScreen().SetKillerText(killer.name);
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        //Debug.Log(col.gameObject.name);
    }

    private void Die()
    {
        isDead = true;
        GetComponent<PlayerSetup>().GetPlayerUI().ToggleRespawnScreen();

        respawnTimer -= Time.deltaTime;
        GetComponent<PlayerSetup>().GetPlayerUI().GetRespawnScreen().SetRespawnTimerText(respawnTimer.ToString());

        //Debug.Log("dieded");

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

         deaths++;

        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        // Time till respawn
        yield return new WaitForSeconds(timeTillRespawn);

        SetDefaults();
        GetComponent<PlayerSetup>().GetPlayerUI().ToggleRespawnScreen();

        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        Debug.Log(transform.name + "respawned");
    }
}
