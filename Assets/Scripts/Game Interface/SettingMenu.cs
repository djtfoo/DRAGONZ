using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SettingMenu : MonoBehaviour {

    public Toggle FullscreenToggle;
	// Use this for initialization
	void Start () {
        PlayerSetting.ScreenHeight = Screen.height;
        PlayerSetting.ScreenWidth = Screen.width;
        PlayerSetting.Fullscreen = Screen.fullScreen;
        FullscreenToggle.isOn = PlayerSetting.Fullscreen;

	}
	public void ChangeResolutionInGame()
    {
        PlayerSetting.Fullscreen = FullscreenToggle.isOn;
        Screen.SetResolution(PlayerSetting.ScreenWidth, PlayerSetting.ScreenHeight, FullscreenToggle.isOn);
    }
    public void Confirm()
    {
        this.gameObject.SetActive(false);
    }
   
	// Update is called once per frame
	void Update () {
	
	}
}
