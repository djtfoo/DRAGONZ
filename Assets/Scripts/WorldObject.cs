using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class WorldObject : MonoBehaviour {
    public float lifeTime;
    public bool LifeTimeEnabled;
   // public bool MultipleChildObjects;
  //  public List<GameObject> ListOfChildObjects;
	// Use this for initialization
	void Start () {
      
        //if(MultipleChildObjects)
        //{ 
        //    foreach(Transform transform in this.transform)
        //    {
        //        transform.gameObject.SetActive(true);
        //        ListOfChildObjects.Add(transform.gameObject);
        //        Debug.Log("YES");
        //    }
        //}
	}
	
	// Update is called once per frame
	void Update () {
	    if (LifeTimeEnabled)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime < 0f)
            {
                Destroy(this.gameObject);
            }
        }
	}
    void Awake()
    {
        //gameObject.SetActive(false);
        LifeTimeEnabled = false;
        //MultipleChildObjects = false;
    }
}
