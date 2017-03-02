using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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

    public float matchTime;
    private bool bStartTimeCheck;

    public int mapSeed;

    Camera sceneCamera;

    GameObject[] playersGO;

    public PlayerUI GetPlayerUI()
    {
        return playerUIInstance.GetComponent<PlayerUI>();
    }

    // Use this for initialization
    void Start()
    {
        string username = "Loading...";

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

            PlayerUI playerUI = playerUIInstance.GetComponent<PlayerUI>();  // PlayerUI component of the PlayerUI instance

            EnergyScript energy = this.gameObject.GetComponent<EnergyScript>();
            energy.energyBarImage = playerUI.energyBar;
            energy.chargeBarImage = playerUI.chargeEnergyBar;

            Health health = this.gameObject.GetComponent<Health>();
            health.HealthImage = playerUI.healthBar;

       //     HealthBarManager healthBarManager = GameObject.FindObjectOfType<HealthBarManager>();
           

            ComboMeter combometer = this.gameObject.GetComponent<ComboMeter>();
            combometer.ComboCounterText = playerUI.comboCounterText;
            combometer.ComboTimerBar = playerUI.comboCounterTimer;

            DragonAttack dragonAttack = this.gameObject.GetComponent<DragonAttack>();
            dragonAttack.raycaster = playerUIInstance.GetComponent<GraphicRaycaster>();

            Radar radar = this.gameObject.GetComponent <Radar>();
            radar = playerUI.radar;

            MoveJoystick joystick = playerUI.joystick.GetComponent<MoveJoystick>();
            this.gameObject.GetComponent<PlayerMovement>().SetJoystick(joystick);

            bStartTimeCheck = false;
        }

        RegisterPlayer();
        GetComponent<Player>().Setup();

        
        if (UserAccountManager.IsLoggedIn)
            username = UserAccountManager.LoggedIn_Username;
        else
            username = transform.name;

        CmdSetUsername(transform.name, username);
    }

    [Command]
    void CmdSetUsername(string playerName, string _username)
    {
        playersGO = GameObject.FindGameObjectsWithTag("Player");
        //RpcSetUserName(0, _username);

        for (int i = 0; i < playersGO.Length; ++i)
        {
            if (playersGO[i].name == playerName)
            {
                playersGO[i].GetComponent<Player>().username = _username;
                //NetworkServer.FindLocalObject(playersGO[i].GetComponent<PlayerSetup>().netId).GetComponent<Player>().username = _username;
                break;
            }
        }
    }

    [ClientRpc]
    void RpcSetUserName(int index, string _username)
    {
        playersGO[index].GetComponent<Player>().username = _username;
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        // Set match timer
        //GetPlayerUI().GetMatchTimer_().matchTimerText.text = matchTime.ToString(); 

        // convert time to minutes & seconds
        int minutesInt = (int)(matchTime / 60f);
        int secondsInt = (int)(matchTime % 60f);
        string minutes = minutesInt.ToString();
        if (minutesInt < 10)
            minutes = "0" + minutes;
        string seconds = secondsInt.ToString();
        if (secondsInt < 10)
            seconds = "0" + seconds;
        GetPlayerUI().GetMatchTimer_().matchTimerText.text = minutes + ":" + seconds;

        if (matchTime > 0)
            bStartTimeCheck = true;

        if (bStartTimeCheck && matchTime <= Mathf.Epsilon)
        {
            GetPlayerUI().GetMatchEndScreen().SetActive(true);
            GetPlayerUI().GetPauseMenuScreen().SetActive(false);
        }

        PlayerUI.playerPos = transform.position;
    }

    [ClientRpc]
    public void RpcSetMatchTime(float _matchTime)
    {
        if (isLocalPlayer)
            matchTime = _matchTime;
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