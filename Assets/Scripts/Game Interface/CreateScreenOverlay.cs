using UnityEngine;
using System.Collections;

public class CreateScreenOverlay : MonoBehaviour {

    public GameObject overlayToCreate;
    public HandleOverlay overlayHandler;

    public void CreateOverlay()
    {
        //OverlayActive.SetOverlayActive(true);

        // check if there is an overlay first
        if (overlayHandler.GetOverlay() != null)
        {
            overlayHandler.DestroyOverlay();
        }

        GameObject overlay = (GameObject)Instantiate(overlayToCreate, new Vector3(0, 0, 0), Quaternion.identity);
        overlay.transform.SetParent(this.transform.parent);   // set to this button's canvas
        overlay.transform.localPosition = overlayToCreate.transform.localPosition;

        //popUp.GetChild(0).localScale = new Vector3(0.1f, 0.1f, 1);

        overlay.transform.localScale = new Vector3(1, 1, 1);

        RectTransform transform = overlay.GetComponent<RectTransform>();
        transform.sizeDelta = new Vector2(0f, 0f);

        // pass it over to the handler
        overlayHandler.SetOverlay(overlay);
    }

}
