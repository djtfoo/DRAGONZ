using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// takes noise map and apply it to a texture; apply the texture to the plane

public class MapDisplay : MonoBehaviour {

    public Renderer textureRenderer;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public MeshCollider meshCollider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DrawTexture(Texture2D texture)
    {
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void DrawMesh(MeshData meshData, Texture2D texture) {
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
        meshCollider.sharedMesh = meshFilter.sharedMesh;

        //meshRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

}
