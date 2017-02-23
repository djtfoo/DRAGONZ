using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetTerrainTexture : MonoBehaviour {

    public RawImage thisImage;

	// Start is called upon initialisation
	void Start () {
        thisImage.texture = GetTerrainTexture.GetTexture2D();
	}
}
