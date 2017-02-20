using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class RadarObject
{
    public GameObject[] owner;
    public Image currentIcon;
    public Image icon;
    public Image iconHigher;
    public Image iconLower;
}

public class Radar : MonoBehaviour
{
    public Image icon;
    public Image iconHigher;
    public Image iconLower;

    public GameObject player;
    float mapScale = 0.05f;
    int offsetY = 100;

    private void SetPlayer(GameObject setPlayer)
    {
        this.player = setPlayer;
    }

    // Array to store all the gameobjects with the tag
    GameObject[] worldObject; //= GameObject.FindGameObjectsWithTag("WorldObject");

    public static List<RadarObject> radObjects = new List<RadarObject>();

    void RegisterRadarObject(Image icon ,Image iconHigher, Image iconLower)
    {
        Image middleIcon = Instantiate(icon);
        Image higherIcon = Instantiate(iconHigher);
        Image lowerIcon = Instantiate(iconLower);
        // Add into the list
        radObjects.Add(new RadarObject() { owner = worldObject, icon = middleIcon, iconHigher = higherIcon , iconLower=lowerIcon, currentIcon = middleIcon });
    }

    //public static void RemoveRadarObject(GameObject o)
    //{
    //    List<RadarObject> newList = new List<RadarObject>();
    //    for (int i = 0; i < radObjects.Count; i++)
    //    {
    //        if (radObjects[i].owner == o)
    //        {
    //            Destroy(radObjects[i].icon);
    //            continue;
    //        }
    //        else
    //            newList.Add(radObjects[i]);
    //    }

    //    radObjects.RemoveRange(0, radObjects.Count);
    //    radObjects.AddRange(newList);
    //}

    void DrawRadarDots()
    {
        foreach (RadarObject ro in radObjects)
        {
            foreach (GameObject go in worldObject)
            {
                Vector3 radarPos = (go.transform.position - player.transform.position);

                float distToObject = Vector3.Distance(player.transform.position, go.transform.position) * mapScale;
                float deltay = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg - 270 - player.transform.eulerAngles.y;
                radarPos.x = distToObject * Mathf.Cos(deltay * Mathf.Deg2Rad) * -1;
                radarPos.z = distToObject * Mathf.Sin(deltay * Mathf.Deg2Rad);
                ro.currentIcon.gameObject.transform.SetParent(this.gameObject.transform);
                //ro.currentIcon.gameObject.transform.SetParent(GameObject.Find("Canvas").transform);
                ro.currentIcon.gameObject.transform.position = new Vector3(radarPos.x, radarPos.z, 0) + this.transform.position;
                //ro.transform.SetParent();
            }

        }
    }

    // Use this for initialization
    void Start()
    {
        worldObject = GameObject.FindGameObjectsWithTag("WorldObject");
        RegisterRadarObject(icon, iconHigher, iconLower);

        // find localplayer & set
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; ++i)
        {
            if (players[i].layer == 8)  // localplayer layer
            {
                this.SetPlayer(players[i]);
            }
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        DrawRadarDots();
        //Debug.Log("hey");

       foreach(RadarObject ro in radObjects)
       {
           foreach(GameObject go in worldObject)
           {
               // the position of all the gameobjects on the map
               Vector3 radarPos = go.transform.position;
               //Debug.Log("Radar Pos"+radarPos);

               // the position of players
               Player playerScript = player.GetComponent<Player>();
               Vector3 playerPos = PlayerUI.playerPos;//playerScript.transform.position;
             //  Debug.Log("PlayerPos:" + playerPos);

               // check for gameobject above the player
               if(radarPos.y > (playerPos.y + offsetY))
               {
                  
                   ro.iconHigher.gameObject.SetActive(true);
                   ro.iconLower.gameObject.SetActive(false);
                   ro.icon.gameObject.SetActive(false); 
                   ro.currentIcon = ro.iconHigher;
                  // ro.currentIcon.sprite = ro.iconHigher.sprite;
                   //Debug.Log("Higher"); 
               }
               // check for gameobject below the player
               else if(radarPos.y < (playerPos.y - offsetY))
               {
                  
                   ro.iconHigher.gameObject.SetActive(false);
                   ro.iconLower.gameObject.SetActive(true);
                   ro.icon.gameObject.SetActive(false);
                   ro.currentIcon = ro.iconLower;
                    // ro.currentIcon.sprite = ro.iconLower.sprite;
                   //Debug.Log("Lower");

               }
               // in the middle
               else
               {
                  
                   ro.iconHigher.gameObject.SetActive(false);
                   ro.iconLower.gameObject.SetActive(false);
                   ro.icon.gameObject.SetActive(true); 
                   ro.currentIcon = ro.icon;
                    // ro.currentIcon.sprite = ro.icon.sprite;
                   //Debug.Log("Middle");
               }
           }
       }

    }
}

