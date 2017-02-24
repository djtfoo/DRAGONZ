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

    //[SerializeField]
    //List<WorldObject> SpawnedGOList;
    public int AmtOfGameObjects;
	// Use this for initialization
	//void Start () {
    //
    //    GameObject terrain = GameObject.FindGameObjectWithTag("Terrain");
    //
    //    foreach (WorldObject WO in TypesOfGameObjects)
    //    {
    //        for (int a = 0; a < AmtOfGameObjects; a++)
    //        {
    //
    //            WorldObject temp = Instantiate(WO);
    //            temp.gameObject.SetActive(true);
    //            temp.LifeTimeEnabled = true;
    //            SpawnedGOList.Add(temp);
    //        }
    //    }
    //    for(int a=0; a<SpawnedGOList.Count; a++)
    //    {
    //        bool haveNotSpawned = true;
    //        while (haveNotSpawned)
    //        {
    //            float x = Random.Range(0, 1);
    //            float z = Random.Range(0, 1);
    //
    //            float height = GetTerrainHeight.GetHeight(terrain, x, z);
    //            //if (height )
    //            //Vector3 temp = new Vector3(x, G, Random.Range(0, 1));
    //
    //        }
    //
    //
    //       //Vector3 temp =()
    //    }
	//}
	//
	//// Update is called once per frame
	//void Update () {
    //    foreach (WorldObject Wo in SpawnedGOList)
    //    {
    //        if (Wo.gameObject.activeSelf)
    //        {
    //            Wo.lifeTime -= Time.deltaTime;
    //            if (Wo.lifeTime <= 0)
    //            {
    //                Wo.gameObject.SetActive(false);
    //            }
    //        }
    //    }
	//}
}
