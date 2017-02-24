using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
     public  Radar radar;

    [SerializeField]
    GameObject respawnScreen;

    public Image energyBar;
    public Image chargeEnergyBar;
    public Image healthBar;
    public Image comboCounterTimer;
    public Text comboCounterText;
    public static Vector3 playerPos;

    void Start()
    {
        OverlayActive.SetOverlayActive(false);
        //PauseMenu.isOn = false;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
	}

    void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        OverlayActive.SetOverlayActive(!OverlayActive.IsOverlayActive());
        //PauseMenu.isOn = pauseMenu.activeSelf;
    }

    public void ToggleRespawnScreen()
    {
        respawnScreen.SetActive(!respawnScreen.activeSelf);
        OverlayActive.SetOverlayActive(!OverlayActive.IsOverlayActive());
        Debug.Log("toggled");
    }

    public RespawnScreen GetRespawnScreen()
    {
        return respawnScreen.GetComponent<RespawnScreen>();
    }
}
