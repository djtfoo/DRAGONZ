using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetTerrainTexture : MonoBehaviour {

    public RawImage thisImage;
    public Radar radar;

	// Start is called upon initialisation
	void Start () {
        thisImage.texture = GetTerrainTexture.GetTexture2D();
	}

    // Update is called every frame
    void Update()
    {
        Vector3 playerPos = radar.player.transform.position;
        float mapScale = radar.GetMapScale();
        float offset = 100f;
        Vector3 rotation = radar.player.transform.eulerAngles;//radar.GetPlayerRotation();

        // scale with map scale
        thisImage.transform.localScale = new Vector3(offset * mapScale, offset * mapScale, 1f);
        // rotate with player;
        //thisImage.transform.Rotate(0,0,);//*thisImage.transform.loca);
        thisImage.transform.eulerAngles = new Vector3(0, 0, rotation.y);

        // terrain image follow player

       

       
      
       
    }
}

// step 1: make sure terrain can zoom in & out w/ map scale
// step 2: make sure terrain is following player correctly
// step 3: make sure the terrain image is scaled correctly w/ world objects