using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GraphicsSettingsMenu : MonoBehaviour {

    public Toggle FullscreenToggle;
    public Toggle Shadows;
    //public float shadowDistance;
	// Use this for initialization
	void Start () {
        SettingsData.ScreenHeight = Screen.height;
        SettingsData.ScreenWidth = Screen.width;

#if !UNITY_ANDROID
        SettingsData.isFullscreen = Screen.fullScreen;
        FullscreenToggle.isOn = SettingsData.isFullscreen;
#endif

        SettingsData.shadowDistance = QualitySettings.shadowDistance;
        SettingsData.QualityLevel = QualitySettings.GetQualityLevel();
        if(SettingsData.shadowDistance<=0)
        {
            Shadows.isOn = false;
        }
        else 
        {
            Shadows.isOn = true;
        }
	}
	public void ChangeResolutionInGame()
    {
        if (Shadows.isOn)
            SettingsData.shadowDistance = 150;
        else
            SettingsData.shadowDistance = 0;

        QualitySettings.shadowDistance = SettingsData.shadowDistance;
        QualitySettings.SetQualityLevel(SettingsData.QualityLevel, true);
#if !UNITY_ANDROID
        SettingsData.isFullscreen = FullscreenToggle.isOn;
        Screen.SetResolution(SettingsData.ScreenWidth, SettingsData.ScreenHeight, FullscreenToggle.isOn);
#endif
    }

}
