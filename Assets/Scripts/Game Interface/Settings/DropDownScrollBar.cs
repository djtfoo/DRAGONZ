using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DropDownScrollBar : MonoBehaviour {
    ScrollRect scrollRect;
    Scrollbar scrollbar;
   public float value;
	// Use this for initialization
	void Start () {
        scrollRect = GetComponent<ScrollRect>();

       // scrollbar = scrollRect.verticalScrollbar;
        if (SettingsData.ResolutionDropDownValue == 0)
        {
            scrollRect.verticalNormalizedPosition = 1;
            //scrollbar.value = 1;
        }
        else
        {
            // scrollbar.value = (1-(SettingsData.ResolutionDropDownValue+1 / (SettingsData.AmtOfResolution)));
            scrollRect.verticalNormalizedPosition =(float)(1- (SettingsData.ResolutionDropDownValue  / (SettingsData.AmtOfResolution)));
        }
        //scrollbar.value = 1 - (SettingsData.ResolutionDropDownValue / (SettingsData.AmtOfResolution-1));
        // Debug.Log(scrollbar.value);

    }
	
	// Update is called once per frame
	void Update () {
        //if (SettingsData.MoveScrollBar)
        //{
        //    if (SettingsData.ResolutionDropDownValue == 0)
        //    {
        //        scrollRect.verticalNormalizedPosition = 1;
        //        scrollbar.value = 0;
        //    }
        //    else
        //    {

        //        scrollbar.value = (float)(1 - (SettingsData.ResolutionDropDownValue  / (SettingsData.AmtOfResolution - 1)));
        //        scrollRect.verticalNormalizedPosition = scrollbar.value;
        //    }
        //    SettingsData.MoveScrollBar = false;
        //}
    }
}
