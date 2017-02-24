using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CreateBurntTexture {
    
    //public float timer;
    //public float lifetime;
    //public int AmtOfBurntTexture;

    //BulletTexture, location, Quaternion.FromToRotation(Vector3.forward, hit.point));
    public static void InstantiateBurntTexture(WorldObject burntGO, Vector3 pos, Quaternion Rot)
    {
        WorldObject temp = (WorldObject)MonoBehaviour.Instantiate(burntGO, pos, Rot);
        temp.gameObject.SetActive(true);
        temp.LifeTimeEnabled = true;

       // return (GameObject)temp;
    }
}
