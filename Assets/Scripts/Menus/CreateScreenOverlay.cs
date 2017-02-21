using UnityEngine;
using System.Collections;

public class CreateScreenOverlay : MonoBehaviour {

    public GameObject overlayToCreate;

    public void CreateOverlay()
    {
        GameObject overlay = (GameObject)Instantiate(overlayToCreate, new Vector3(0, 0, 0), Quaternion.identity);
        overlay.transform.SetParent(this.transform.parent);   // set to this button's canvas
        overlay.transform.localPosition = overlayToCreate.transform.localPosition;

        //popUp.GetChild(0).localScale = new Vector3(0.1f, 0.1f, 1);

        overlay.transform.localScale = new Vector3(1, 1, 1);
    }

}
