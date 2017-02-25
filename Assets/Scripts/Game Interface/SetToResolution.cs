using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetToResolution : MonoBehaviour {

    Vector2 canvasSizeDelta;
    Vector2 canvasRefSize;

	// Use this for initialization
	void Start () {
        canvasSizeDelta = this.transform.parent.GetComponent<RectTransform>().sizeDelta;
        canvasRefSize = this.transform.parent.GetComponent<CanvasScaler>().referenceResolution;

        RectTransform rect = this.gameObject.GetComponent<RectTransform>();

        Debug.Log(rect.transform.localPosition);

        rect.sizeDelta = new Vector2(rect.sizeDelta.x / canvasRefSize.x * canvasSizeDelta.x, rect.sizeDelta.y / canvasRefSize.y * canvasSizeDelta.y);
        rect.transform.localPosition = new Vector3(rect.transform.localPosition.x / canvasRefSize.x * canvasSizeDelta.x, rect.transform.localPosition.y / canvasRefSize.y * canvasSizeDelta.y, 0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
