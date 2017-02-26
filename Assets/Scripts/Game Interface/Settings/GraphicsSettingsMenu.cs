using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GraphicsSettingsMenu : MonoBehaviour {

    public Toggle FullscreenToggle;

	// Use this for initialization
	void Start () {
        SettingsData.ScreenHeight = Screen.height;
        SettingsData.ScreenWidth = Screen.width;
        SettingsData.isFullscreen = Screen.fullScreen;
        FullscreenToggle.isOn = SettingsData.isFullscreen;
	}
	public void ChangeResolutionInGame()
    {
        SettingsData.isFullscreen = FullscreenToggle.isOn;
        Screen.SetResolution(SettingsData.ScreenWidth, SettingsData.ScreenHeight, FullscreenToggle.isOn);
    }

}
