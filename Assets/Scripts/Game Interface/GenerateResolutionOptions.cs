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

            if(PlayerSetting.ScreenHeight==res.height && PlayerSetting.ScreenWidth == res.width)
            {
                index = tempIndex;
            }
        }
        GetComponent<Dropdown>().AddOptions(resolutionOptions);
        GetComponent<Dropdown>().itemText.text = PlayerSetting.ScreenWidth.ToString() + " X " +PlayerSetting.ScreenHeight.ToString() ;
        GetComponent<Dropdown>().value = index;
        PlayerSetting.AmtOfResolution = tempIndex;

	}
	public void OnValueChanged()
    {
        PlayerSetting.ScreenHeight = Screen.resolutions[GetComponent<Dropdown>().value].height;
        PlayerSetting.ScreenWidth = Screen.resolutions[GetComponent<Dropdown>().value].width;

    }
	// Update is called once per frame
	void Update () {
	
	}
}
