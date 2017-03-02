using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameplaySettingsMenu : MonoBehaviour {

    public Toggle toggleInvertVertical;
    public Toggle toggleInvertHorizontal;

    // Use this for initialization
    void Start() {
        if (SettingsData.GetInvertVerticalAxis() == -1)
            toggleInvertVertical.isOn = true;
        else
            toggleInvertVertical.isOn = false;

        if (SettingsData.GetInvertHorizontalAxis() == -1)
            toggleInvertHorizontal.isOn = true;
        else
            toggleInvertHorizontal.isOn = false;
    }

    // Update is called once per frame
    void Update() {

    }

    // called by Apply button
    public void ApplyChanges()
    {
        SettingsData.SetInvertVerticalAxis(toggleInvertVertical.isOn);
        SettingsData.SetInvertHorizontalAxis(toggleInvertHorizontal.isOn);
    }

}
