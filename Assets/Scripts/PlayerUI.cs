using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerUI : NetworkBehaviour {

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject scoreboard;

    [SerializeField]
    public Radar radar;

    [SerializeField]
    GameObject respawnScreen;

    public GameObject timer;

    public Image energyBar;
    public Image chargeEnergyBar;
    public Image healthBar;
    public Image comboCounterTimer;
    public Text comboCounterText;
    public Image HealthBarAbovePlayer;
    public Image MaxHealthBarAbovePlayer;
    public static Vector3 playerPos;

    // Android stuff
    [SerializeField]
    public GameObject joystick;
    [SerializeField]
    public GameObject pauseButton;
    [SerializeField]
    public GameObject scoreboardButton;

    void Start()
    {
        NetworkManager.singleton.GetComponent<MatchTimer>().enabled = true;
        OverlayActive.SetOverlayActive(false);

        //PauseMenu.isOn = false;

#if !UNITY_ANDROID
        joystick.SetActive(false);
        pauseButton.SetActive(false);
        scoreboardButton.SetActive(false);
#else
        joystick.SetActive(true);
        pauseButton.SetActive(true);
        scoreboardButton.SetActive(true);
#endif
    }
	
	// Update is called once per frame
	void Update () 
    {
#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))   // alternate method instead of pause button
        {
            TogglePauseMenu();
        }
#else
        if (Input.GetKeyDown(KeyBoardBindings.GetPauseKey()))
        {
            TogglePauseMenu();
        }
        if (Input.GetKeyDown(KeyBoardBindings.GetScoreboardKey()))
        {
            scoreboard.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyBoardBindings.GetScoreboardKey()))
        {
            scoreboard.SetActive(false);
        }
#endif
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        OverlayActive.SetOverlayActive(!OverlayActive.IsOverlayActive());
        //PauseMenu.isOn = pauseMenu.activeSelf;
    }

    public void ToggleScoreboard()
    {
        scoreboard.SetActive(!scoreboard.activeSelf);
    }

    public void ToggleRespawnScreen()
    {
        respawnScreen.SetActive(!respawnScreen.activeSelf);
        OverlayActive.SetOverlayActive(!OverlayActive.IsOverlayActive());
    }

    public RespawnScreen GetRespawnScreen()
    {
        return respawnScreen.GetComponent<RespawnScreen>();
    }

    public GetMatchTimer GetMatchTimer_()
    {
        return timer.GetComponent<GetMatchTimer>();
    }
}
