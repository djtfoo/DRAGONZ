using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public static class CreateBurntTexture {
    
    //public float timer;
    //public float lifetime;
    //public int AmtOfBurntTexture;

    public static WorldObject burntGo = (WorldObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/TestImage.prefab", typeof(WorldObject));

    //BulletTexture, location, Quaternion.FromToRotation(Vector3.forward, hit.point));
    public static void InstantiateBurntTexture(Vector3 pos, Quaternion Rot)
    {
        WorldObject temp = (WorldObject)MonoBehaviour.Instantiate(burntGo, pos, Rot);
        temp.gameObject.SetActive(true);
        temp.LifeTimeEnabled = true;

       // return (GameObject)temp;
    }
}
