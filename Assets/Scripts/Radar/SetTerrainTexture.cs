using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetTerrainTexture : MonoBehaviour {

    public RawImage thisImage;
    public Radar radar;

	// Start is called upon initialisation
	void Start () {
        thisImage.texture = GetTerrainTexture.GetTexture2D();
        MapData.terrainScale = GameObject.Find("Terrain").transform.localScale.x;
	}

    // Update is called every frame
    void Update()
    {
        Vector3 playerPos = radar.player.transform.position;
        playerPos = new Vector3(playerPos.x, playerPos.y, playerPos.z);
        float mapScale = radar.GetMapScale();
        float offset = MapData.terrainScale;
        Vector3 rotation = radar.player.transform.eulerAngles;//radar.GetPlayerRotation();

        // scale with map scale
        thisImage.transform.localScale = new Vector3(-offset * mapScale, offset * mapScale, 1f);
        // rotate with player;
        thisImage.transform.eulerAngles = new Vector3(0, 0, rotation.y + 180f);
        // terrain image follow player
        Vector3 radarPosOffset = radar.player.transform.position;
        float playerDistFromCentre = radarPosOffset.magnitude * mapScale;
        float deltay = Mathf.Atan2(radarPosOffset.x, radarPosOffset.z) * Mathf.Rad2Deg - 270 - rotation.y;
        radarPosOffset.x = -playerDistFromCentre * Mathf.Cos(deltay * Mathf.Deg2Rad) * -1;
        radarPosOffset.z = -playerDistFromCentre * Mathf.Sin(deltay * Mathf.Deg2Rad);
        thisImage.transform.position = new Vector3(radarPosOffset.x, radarPosOffset.z, 0) + radar.transform.position;

    }
}

// step 1: make sure terrain can zoom in & out w/ map scale
// step 2: make sure terrain is following player correctly
// step 3: make sure the terrain image is scaled correctly w/ world objects