using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetResolution : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RectTransform rect = this.gameObject.GetComponent<RectTransform>();
        rect.sizeDelta = this.transform.parent.GetComponent<RectTransform>().sizeDelta;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
