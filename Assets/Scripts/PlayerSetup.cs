using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    [SerializeField]
    GameObject playerUIPrefab;
    private GameObject playerUIInstance;

    public static bool isHost = false; // dont use this for now
    public static long m_networkID;
    public static long m_nodeID;

    [SyncVar]
    private float hostTimer;

    private float updateInterval;
    private GameObject theHost;
    public float matchTime;

    Camera sceneCamera;

    public PlayerUI GetPlayerUI()
    {
        return playerUIInstance.GetComponent<PlayerUI>();
    }

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
            // If we have a main camera and it has been marked as main
            //sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                //sceneCamera.gameObject.SetActive(false);
            }

            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;

            EnergyScript energy = this.gameObject.GetComponent<EnergyScript>();
            PlayerUI playerUI = playerUIInstance.GetComponent<PlayerUI>();
            energy.energyBarImage = playerUI.energyBar;
            energy.chargeBarImage = playerUI.chargeEnergyBar;

            Health health = this.gameObject.GetComponent<Health>();
            health.HealthImage = playerUI.healthBar;

            ComboMeter combometer = this.gameObject.GetComponent<ComboMeter>();
            combometer.ComboCounterText = playerUI.comboCounterText;
            combometer.ComboTimerBar = playerUI.comboCounterTimer;

            Radar radar = this.gameObject.GetComponent <Radar>();
            radar = playerUI.radar;
        }

        RegisterPlayer();
        GetComponent<Player>().Setup();
    }

    [Client]
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (isServer)
        {
            GetPlayerUI().GetMatchTimer_().matchTimerText.text = NetworkManager.singleton.GetComponent<MatchTimer>().GetSeconds().ToString();

            updateInterval += Time.deltaTime;
            if (updateInterval > 0.11f) // 9 times per sec (default unity send rate)
            {
                updateInterval = 0;
                RpcSync(matchTime);
            }
        }
        else
        {
            //GameObject[] playersGO = GameObject.FindGameObjectsWithTag("Player");
            //for (int i = 0; i < playersGO.Length; ++i)
            //{
            //    if (playersGO[i].GetComponent<PlayerSetup>().hasAuthority)
            //    {
            //        theHost = playersGO[i];
            //        break;
            //    }
            //    //NetworkManager.singleton.GetComponent<MatchTimer>().SetTime();
            //}

            // Set timer to theHost's timer
            //matchTime = theHost.GetComponent<PlayerSetup>().matchTime;
            

           matchTime = hostTimer;
           GetPlayerUI().GetMatchTimer_().matchTimerText.text = matchTime.ToString();
        }

        PlayerUI.playerPos = transform.position;
        //Debug.Log(transform.position);
    }

    [ClientRpc]
    void RpcSync(float _matchTime)
    {
        hostTimer = _matchTime;
    }

    void RegisterPlayer()
    {
        string _ID = "Player " + GetComponent<NetworkIdentity>().netId;
        transform.name = _ID;
        Debug.Log("Registered " + _ID);
    }

    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    void OnDisable()
    {
        Destroy(playerUIInstance);

        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }

    void OnDisconnectedFromServer()
    {
        Destroy(gameObject); // not working
    }
}