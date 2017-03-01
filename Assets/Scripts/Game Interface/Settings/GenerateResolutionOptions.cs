using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class GenerateResolutionOptions : MonoBehaviour {

    List<string> resolutionOptions = new List<string>();
    public  int tempIndex = 0;
	// Use this for initialization
	void Start () {
        int index = 0;
        foreach (Resolution res in Screen.resolutions)
        {
            tempIndex++; 

            string temp =  res.width.ToString()+ " X " + res.height.ToString();
            resolutionOptions.Add(temp);

            if(SettingsData.ScreenHeight==res.height && SettingsData.ScreenWidth == res.width)
            {
                index = tempIndex-1;
            }
        }
        GetComponent<Dropdown>().AddOptions(resolutionOptions);
        GetComponent<Dropdown>().itemText.text = SettingsData.ScreenWidth.ToString() + " X " + SettingsData.ScreenHeight.ToString() ;
        GetComponent<Dropdown>().value = index;
        SettingsData.ResolutionDropDownValue = index;
        SettingsData.AmtOfResolution = tempIndex;

	}
	public void OnValueChanged()
    {
        SettingsData.ScreenHeight = Screen.resolutions[GetComponent<Dropdown>().value].height;
        SettingsData.ScreenWidth = Screen.resolutions[GetComponent<Dropdown>().value].width;
        SettingsData.ResolutionDropDownValue = GetComponent<Dropdown>().value;
        SettingsData.MoveScrollBar = true;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
