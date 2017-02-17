using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class RadarObject
{
    public Image currentIcon;
    public Image icon { get; set; }
    public Image iconHigher;
    public Image iconLower;
    public GameObject owner { get; set; }
}

public class Radar : MonoBehaviour
{
    public GameObject player;
    float mapScale = 0.05f;
    int offsetY = 100;

    public static List<RadarObject> radObjects = new List<RadarObject>();

    public static void RegisterRadarObject(GameObject o, Image i,Image iconHigher, Image iconLower)
    {
        Image image = Instantiate(i);
        Image higherImage = Instantiate(iconHigher);
        Image lowerImage = Instantiate(iconLower);
        radObjects.Add(new RadarObject() { owner = o, icon = image, iconHigher=higherImage , iconLower=lowerImage, currentIcon = image });
    }

    public static void RemoveRadarObject(GameObject o)
    {
        List<RadarObject> newList = new List<RadarObject>();
        for (int i = 0; i < radObjects.Count; i++)
        {
            if (radObjects[i].owner == o)
            {
                Destroy(radObjects[i].icon);
                continue;
            }
            else
                newList.Add(radObjects[i]);
        }

        radObjects.RemoveRange(0, radObjects.Count);
        radObjects.AddRange(newList);
    }

    void DrawRadarDots()
    {
        foreach (RadarObject ro in radObjects)
        {
            Vector3 radarPos = (ro.owner.transform.position - player.transform.position);
            float distToObject = Vector3.Distance(player.transform.position, ro.owner.transform.position) * mapScale;
            float deltay = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg - 270 - player.transform.eulerAngles.y;
            radarPos.x = distToObject * Mathf.Cos(deltay * Mathf.Deg2Rad) * -1;
            radarPos.z = distToObject * Mathf.Sin(deltay * Mathf.Deg2Rad);
         //   ro.currentIcon = ro.icon;
            ro.currentIcon.gameObject.transform.SetParent(this.gameObject.transform);
            ro.currentIcon.gameObject.transform.position = new Vector3(radarPos.x, radarPos.z, 0) + this.transform.position;
        }
    }

    // Use this for initialization
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        DrawRadarDots();
        //Debug.Log("hey");

       foreach(RadarObject ro in radObjects)
       {
           // the position of all the gameobjects on the map
           Vector3 radarPos = ro.owner.transform.position;
           //Debug.Log(radarPos.y);

           // the position of players
           Player playerScript = player.GetComponent<Player>();
           Vector3 playerPos = playerScript.GetPosition();

           // check for gameobject above the player
           if(radarPos.y > (playerPos.y + offsetY))
           {
               // render a green
               ro.currentIcon = ro.iconHigher;
               ro.iconHigher.gameObject.SetActive(true);
               ro.iconLower.gameObject.SetActive(false);
               ro.icon.gameObject.SetActive(false);
           }
           // check for gameobject below the player
           else if(radarPos.y < (playerPos.y - offsetY))
           {
               // render a blue dot
               ro.currentIcon = ro.iconLower;
               ro.iconHigher.gameObject.SetActive(false);
               ro.iconLower.gameObject.SetActive(true);
               ro.icon.gameObject.SetActive(false);

           }
           // in the middle
           else
           {
               // render a red dot
               ro.currentIcon = ro.icon;
               ro.iconHigher.gameObject.SetActive(false);
               ro.iconLower.gameObject.SetActive(false);
               ro.icon.gameObject.SetActive(true);
           }
       }

    }
}

