using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AndroidGameplaySettingsMenu : MonoBehaviour {

    public Text joystickText;
    public GameObject setJoystickToLeft;
    public GameObject setJoystickToRight;

	// Use this for initialization
	void Start () {
	    if (SettingsData.IsJoystickLeftSide())
        {
            setJoystickToLeft.SetActive(false);
            setJoystickToRight.SetActive(true);
            joystickText.text = "Joystick On Left";
        }
        else
        {
            setJoystickToLeft.SetActive(true);
            setJoystickToRight.SetActive(false);
            joystickText.text = "Joystick On Right";
        }
	}

    public void SetJoystickPositionToggle()
    {
        setJoystickToLeft.SetActive(!setJoystickToLeft.activeSelf);
        setJoystickToRight.SetActive(!setJoystickToRight.activeSelf);

        if (joystickText.text == "Joystick On Left")
        {
            joystickText.text = "Joystick On Right";
        }
        else
        {
            joystickText.text = "Joystick On Left";
        }
    }

    // Called by Apply button
    public void ApplySettingsChange()
    {
        if (setJoystickToLeft.activeSelf)   // set to left is active means joystick is currently on the right
        {
            SettingsData.SetIsJoystickLeftSide(false);
        }
        else
        {
            SettingsData.SetIsJoystickLeftSide(true);
        }

        // Apply to joystick if in the game scene
        GameObject joystick = GameObject.Find("Joystick");
        if (joystick != null) {
            joystick.GetComponent<InitialiseJoystick>().SetJoystickPos();
        }
    }

}
