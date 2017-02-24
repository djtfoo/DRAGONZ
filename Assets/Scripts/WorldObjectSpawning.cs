using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct WorldObjectToSpawn
{
    public WorldObject wo;
    public int quantity;
}

public class WorldObjectSpawning : MonoBehaviour {
    public List<WorldObjectToSpawn> TypesOfGameObjects;
	// Use this for initialization
    GameObject terrain;
	void Start () {
        terrain = GameObject.FindGameObjectWithTag("Terrain");
		//Random.seed = 42;

        foreach (WorldObjectToSpawn WO in TypesOfGameObjects)
        {
           for(int a=0; a< WO.quantity;a++)	
           {
               while (true)
               {
                   Debug.Log("FUCK YEAH");
                   float x = UnityEngine.Random.Range(-200f * 120, 200f * 120);
                  // Debug.Log(x);

                   float z = UnityEngine.Random.Range(-200f * 120, 200f * 120);
                 //  Debug.Log(z);
                   float y = GetTerrainHeight.GetHeight(terrain, x, z);
                   Debug.Log(y);
                   if (y > 0.35f && y < 0.58f)
                   {
						Vector3 Pos = new Vector3(x, y * terrain.transform.localScale.y + 0.5f * WO.wo.transform.localScale.y, z);
                       Instantiate(WO.wo, Pos, Quaternion.identity); // Still need positioning
                       break;
                   }
               }
           }
        }
	}
	//
	//// Update is called once per frame
	void Update () {
    
	}
}
