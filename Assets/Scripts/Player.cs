using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour
{
    private bool isDead = false;
    private bool realIsDead = false;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    public float timeTillRespawn;
    private float respawnTimer;

    public int kills;
    public int deaths;
    private GameObject killer;
    private float updateInterval;

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
        if (killer != null)
            killer.name = "";
        killer = null;
        isDead = false;

        GetComponent<PlayerMovement>().SetDefault();
        GetComponent<Health>().SetDefault();

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
            renderer.enabled = true;

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

    void OnOverlayActive(bool _enabled)
    {
        GetComponent<PlayerMovement>().enabled = _enabled;
        GetComponent<DragonAttack>().enabled = _enabled;
    }

    // Update is called once per frame
    [Client]
    void Update()
    {
        if (isLocalPlayer)
        {
            if (OverlayActive.IsOverlayActive())
                OnOverlayActive(false);
            else
                OnOverlayActive(true);

            if (GetComponent<Health>().currentHealth <= 0.0f && isDead == false)
            {
                Die();
            }
            else if (isDead)
            {
                respawnTimer -= Time.deltaTime;
                GetComponent<PlayerSetup>().GetPlayerUI().GetRespawnScreen().SetRespawnTimerText(respawnTimer.ToString());

                if (respawnTimer <= Mathf.Epsilon)
                    Respawn();
            }

            // For testing
            if (Input.GetKeyDown(KeyCode.K))
            {
                GetComponent<Health>().currentHealth = 0;
                SetKiller(gameObject);
            }

            if (killer != null)
            {
                if (killer.name != this.name)
                    GetComponent<PlayerSetup>().GetPlayerUI().GetRespawnScreen().SetKillerText(killer.name);
                else
                    GetComponent<PlayerSetup>().GetPlayerUI().GetRespawnScreen().SetKillerText("Myself");
            }
            else
                Debug.Log("no killer");

            updateInterval += Time.deltaTime;
            if (updateInterval > 0.11f) // 9 times per sec (default unity send rate)
            {
                updateInterval = 0;
                CmdSync(isDead);
            }
        }
        else
        {
            if (GetComponent<Health>().currentHealth <= 0.0f)
                isDead = true;

            if (killer != null)
            {
                SetKiller(killer);
            }

            RemotePlayerDead(isDead);

            isDead = realIsDead;
        }
    }

    [Command]
    void CmdSync(bool _isDead)
    {
        realIsDead = _isDead;
    }

    void RemotePlayerDead(bool _isDead)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
            renderer.enabled = !_isDead;

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = !_isDead;
    }

    void OnCollisionEnter(Collision col)
    {
        //Debug.Log(col.gameObject.name);
    }

    private void Die()
    {
        deaths++;
        isDead = true;
        GetComponent<PlayerSetup>().GetPlayerUI().ToggleRespawnScreen();
        //Debug.Log("dieded");

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
            renderer.enabled = false;

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;
    }

    void Respawn()
    {
        SetDefaults();
        GetComponent<PlayerSetup>().GetPlayerUI().ToggleRespawnScreen();

        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        Debug.Log(transform.name + "respawned");
    }
}
