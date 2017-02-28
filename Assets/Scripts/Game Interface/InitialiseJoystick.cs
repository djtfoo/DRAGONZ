using UnityEngine;
using System.Collections;

public class InitialiseJoystick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    if (!SettingsData.IsJoystickLeftSide())
        {
            Debug.Log("BOO");

            RectTransform rect = this.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(1, 0);
            rect.anchorMax = new Vector2(1, 0);
            rect.anchoredPosition = new Vector2(-100f, 120f);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
