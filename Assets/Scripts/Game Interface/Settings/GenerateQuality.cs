using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class GenerateQuality : MonoBehaviour {
    List<string> qualityOptions = new List<string>();
    public int tempIndex = 0;
    public Scrollbar scrollbar;

	// Use this for initialization
	void Start () {
        int index=0;
        foreach (string temp in QualitySettings.names)
        {
            tempIndex++;
            qualityOptions.Add(temp);
            if (temp == QualitySettings.names[SettingsData.QualityLevel])
            {
                index = tempIndex - 1;
            }
        }
        GetComponent<Dropdown>().AddOptions(qualityOptions);
        GetComponent<Dropdown>().itemText.text = QualitySettings.names[SettingsData.QualityLevel].ToString();
        GetComponent<Dropdown>().value = index;
        // GetComponent<Dropdown>().

    }
    public void OnValueChanged()
    {
        SettingsData.QualityLevel = GetComponent<Dropdown>().value;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
