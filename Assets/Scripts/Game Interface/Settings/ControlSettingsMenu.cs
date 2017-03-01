using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ControlSettingsMenu : MonoBehaviour {

    public Text forwardKey;
    public Text backwardKey;
    public Text attackKey;
    public Text chargedAttackKey;
    public Text pauseKey;
    public Text zoomInKey;
    public Text zoomOutKey;
    public Text showScoreboardKey;

    GameObject selectedButton = null;

    // Initialisation
    void Start()
    {
        forwardKey.text = KeyBoardBindings.GetForwardKey().ToString();
        backwardKey.text = KeyBoardBindings.GetBackwardKey().ToString();
        attackKey.text = KeyBoardBindings.GetAttackKey().ToString();
        chargedAttackKey.text = KeyBoardBindings.GetChargedAttackKey().ToString();
        pauseKey.text = KeyBoardBindings.GetPauseKey().ToString();
        zoomInKey.text = KeyBoardBindings.GetZoomInKey().ToString();
        zoomOutKey.text = KeyBoardBindings.GetZoomOutKey().ToString();
        showScoreboardKey.text = KeyBoardBindings.GetScoreboardKey().ToString();
    }

    private void Update()
    {
        if (selectedButton)
        {
            foreach (KeyCode vkey in System.Enum.GetValues(typeof(KeyCode))) {
                if (Input.GetKey(vkey)) {
                    selectedButton.transform.GetChild(0).GetComponent<Text>().text = vkey.ToString();
                    selectedButton.GetComponent<ChangeButtonColor>().ChangeNormalColorToAnotherColor(1);
                    selectedButton = null;
                    break;
                }
            }
        }
    }

    // Method called by the Apply button
	public void SetNewKeyBindings()
    {
        Dictionary<string, KeyCode> keyBindings = new Dictionary<string, KeyCode>();

        keyBindings.Add("ForwardKey", (KeyCode)System.Enum.Parse(typeof(KeyCode), forwardKey.text));
        keyBindings.Add("BackwardKey", (KeyCode)System.Enum.Parse(typeof(KeyCode), backwardKey.text));
        keyBindings.Add("AttackKey", (KeyCode)System.Enum.Parse(typeof(KeyCode), attackKey.text));
        keyBindings.Add("ChargedAttackKey", (KeyCode)System.Enum.Parse(typeof(KeyCode), chargedAttackKey.text));
        keyBindings.Add("PauseKey", (KeyCode)System.Enum.Parse(typeof(KeyCode), pauseKey.text));
        keyBindings.Add("ZoomInKey", (KeyCode)System.Enum.Parse(typeof(KeyCode), zoomInKey.text));
        keyBindings.Add("ZoomOutKey", (KeyCode)System.Enum.Parse(typeof(KeyCode), zoomOutKey.text));
        keyBindings.Add("ScoreboardKey", (KeyCode)System.Enum.Parse(typeof(KeyCode), showScoreboardKey.text));

        SettingsData.SaveKeyBindings(keyBindings);
    }

    // Which key to set
    public void SetSelectedButton(GameObject button)
    {
        selectedButton = button;
    }

}
