using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeatTransfer : MonoBehaviour {
    public float currentTemperature,DistanceForTransfer;
    public static List<GameObject> m_GoList ;
	// Use this for initialization
	void Start () {

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("flammable"))
        {
            m_GoList.Add(go);
        }
	}
	
	// Update is called once per frame
	void Update () {
           //if(m_GoList.Length != GameObject.FindGameObjectsWithTag("flammable").Length) //Update List when new Objects are spawned/deleted
           //{

           //}
	}
    public static void AddToGoList(GameObject Go)
    {
        m_GoList.Add(Go);
    }
    public static void DeleteToGoList(GameObject other)
    {
        foreach (GameObject go in m_GoList)
        {
            if (go == other)
            {
                m_GoList.Remove(other);
            }
        }
    }
}
