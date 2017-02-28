using UnityEngine;
using System.Collections;

public class InitialiseJoystick : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SetJoystickPos();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetJoystickPos()
    {
        if (!SettingsData.IsJoystickLeftSide())
        {
            RectTransform rect = this.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(1, 0);
            rect.anchorMax = new Vector2(1, 0);
            rect.anchoredPosition = new Vector2(-100f, 120f);
        }
        else
        {
            RectTransform rect = this.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(0, 0);
            rect.anchoredPosition = new Vector2(100f, 120f);
        }
    }

}
