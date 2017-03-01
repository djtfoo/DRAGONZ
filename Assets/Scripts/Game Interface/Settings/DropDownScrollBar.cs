using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DropDownScrollBar : MonoBehaviour {
    Scrollbar scrollbar;
   public float value;
	// Use this for initialization
	void Start () {
      scrollbar = GetComponent<Scrollbar>(); 
        if (SettingsData.MoveScrollBar)
        {
            value = SettingsData.ResolutionDropDownValue;
            scrollbar.value = 1 - (value / SettingsData.AmtOfResolution-1);
            SettingsData.MoveScrollBar = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
      
	}
}
