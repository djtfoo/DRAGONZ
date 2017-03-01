using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour
{
    [SyncVar]
    private bool isDead;

    private bool real_isDead = false;

    [SyncVar]
    public bool hasRespawned;

    private bool real_hasRespawned;
    private float updateInterval;

    [Command]
    void CmdSync(bool _isDead, bool _hasRespawned)
    {
        real_isDead = _isDead;
        real_hasRespawned = _hasRespawned;
    }

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    public float timeTillRespawn;
    private float respawnTimer;

    private int kills;
    private int deaths;
    private string killerName;

    [SyncVar]
    public string username;

    public void SetKills(int _kills)
    {
        kills = _kills;
    }

    public int GetKills()
    {
        return kills;
    }

    public void SetDeaths(int _deaths)
    {
        deaths = _deaths;
    }

    public int GetDeaths()
    {
        return deaths;
    }

    public int GetBestCombo()
    {
        return 0;
    }

    [ClientRpc]
    public void RpcSetKillerName(string _killerName)
    {
        killerName = _killerName;
    }

    [ClientRpc]
    public void RpcIncreaseStatsCount(string type)
    {
        if (type == "Kills")
            kills++;
        else if (type == "Deaths")
            deaths++;
        else if (type == "Combo")
            ;
        else
            Debug.LogError("RpcIncreaseStatsCount: Undefined type");
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

    public void SetIsDead(bool _isDead)
    {
        isDead = _isDead;
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    public bool GetDeadStatus() // GetIsDead() isn't working properly
    {
        return (GetComponent<Health>().currentHealth <= Mathf.Epsilon);
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
    void Update()
    {
        if (isLocalPlayer)
        {
            OnOverlayActive(!OverlayActive.IsOverlayActive());

            if (GetComponent<Health>().currentHealth <= 0.0f && isDead == false)
            {
                Die();
            }
            else if (isDead)
            {
                if (killerName != "")
                {
                    if (killerName != this.name)
                        GetComponent<PlayerSetup>().GetPlayerUI().GetRespawnScreen().SetKillerText(killerName);
                    else
                        GetComponent<PlayerSetup>().GetPlayerUI().GetRespawnScreen().SetKillerText("Myself");
                }

                respawnTimer -= Time.deltaTime;
                GetComponent<PlayerSetup>().GetPlayerUI().GetRespawnScreen().SetRespawnTimerText(((int)respawnTimer).ToString());

                if (respawnTimer <= 1f)
                    Respawn();
            }

            // For testing
            if (Input.GetKeyDown(KeyCode.K))
            {
                GetComponent<Health>().currentHealth = 0;
                killerName = this.gameObject.name;
            }

            updateInterval += Time.deltaTime;
            if (updateInterval > 0.11f)
            {
                CmdSync(isDead, hasRespawned);
            }
        }
        else
        {
            isDead = real_isDead;
            hasRespawned = real_hasRespawned;
            RemotePlayerDead();
        }
    }

    void RemotePlayerDead()
    {
        if (isDead)
        {
            GetComponent<Health>().SetDefault();
        }
        else if (hasRespawned)
        {
            //GetComponent<Health>().SetDefault();

            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
                renderer.enabled = true;

            Collider col = GetComponent<Collider>();
            if (col != null)
                col.enabled = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        //Debug.Log(col.gameObject.name);
    }

    public void SetDefaults()
    {
        respawnTimer = timeTillRespawn + 1;
        killerName = "";
        isDead = false;
        hasRespawned = false;

        GetComponent<PlayerMovement>().SetDefault();
        GetComponent<Health>().SetDefault();

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }
    }

    private void Die()
    {
        //deaths++;
        isDead = true;
        hasRespawned = false;
        GetComponent<PlayerSetup>().GetPlayerUI().ToggleRespawnScreen();

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
        hasRespawned = true;
        GetComponent<PlayerSetup>().GetPlayerUI().ToggleRespawnScreen();

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
            renderer.enabled = true;

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = true;

        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        //Debug.Log(transform.name + " respawned");
    }
}
