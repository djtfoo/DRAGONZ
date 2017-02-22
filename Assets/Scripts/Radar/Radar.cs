using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class RadarIcon
{
    public Image currentIcon;
    public Image icon;
    public Image iconHigher;
    public Image iconLower;

    public void DestroySelf()
    {
        GameObject.Destroy(icon.gameObject);
        GameObject.Destroy(iconHigher.gameObject);
        GameObject.Destroy(iconLower.gameObject);
        GameObject.Destroy(currentIcon.gameObject);
    }
}

public class Radar : MonoBehaviour
{
    public Image icon;
    public Image iconHigher;
    public Image iconLower;

    public GameObject player;
    float mapScale = 0.05f;
    int offsetY = 100;

    public static List<GameObject> worldObject = new List<GameObject>();
    public static List<RadarIcon> radIcons = new List<RadarIcon>();


    private void SetPlayer(GameObject setPlayer)
    {
        this.player = setPlayer;
    }

    void RegisterRadarObject(Image icon ,Image iconHigher, Image iconLower)
    {
        Image middleIcon = Instantiate(icon);
        Image higherIcon = Instantiate(iconHigher);
        Image lowerIcon = Instantiate(iconLower);
        // Add into the radar icon list
        radIcons.Add(new RadarIcon() { icon = middleIcon, iconHigher = higherIcon , iconLower=lowerIcon, currentIcon = middleIcon });
    }

    public static void RemoveRadarObject(GameObject go)
    {
        // loop through world objects array
        for (int i = 0; i < worldObject.Count; ++i)
        {
            if (worldObject[i] == go)
            {
                //Destroy(go);
                worldObject.RemoveAt(i);
                radIcons[i].DestroySelf();
                radIcons.RemoveAt(i);
                Debug.Log("hey");
                break;
            }
        }
    }

    void DrawRadarDots()
    {
        for (int i = 0; i < worldObject.Count; ++i)
        {
            Vector3 radarPos = (worldObject[i].transform.position - player.transform.position);

            float distToObject = Vector3.Distance(player.transform.position, worldObject[i].transform.position) * mapScale;
            float deltay = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg - 270 - player.transform.eulerAngles.y;
            radarPos.x = distToObject * Mathf.Cos(deltay * Mathf.Deg2Rad) * -1;
            radarPos.z = distToObject * Mathf.Sin(deltay * Mathf.Deg2Rad);

            radIcons[i].currentIcon.gameObject.transform.SetParent(this.gameObject.transform);
            radIcons[i].currentIcon.gameObject.transform.position = new Vector3(radarPos.x, radarPos.z, 0) + this.transform.position;
        }
    }

    // Use this for initialization
    void Start()
    {
        GameObject[] worldObjArr;
        worldObjArr = GameObject.FindGameObjectsWithTag("WorldObject");
        for (int i = 0; i < worldObjArr.Length; ++i)
        {
            worldObject.Add(worldObjArr[i]);
            RegisterRadarObject(icon, iconHigher, iconLower);
        }


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

        for (int i = 0; i < worldObject.Count; ++i)
        {
            // the position of all the gameobjects on the map
            Vector3 radarPos = worldObject[i].transform.position;

            // the position of players
            Player playerScript = player.GetComponent<Player>();
            Vector3 playerPos = PlayerUI.playerPos;//playerScript.transform.position;
      

            // check for gameobject above the player
            if (radarPos.y > (playerPos.y + offsetY))
            {

                radIcons[i].iconHigher.gameObject.SetActive(true);
                radIcons[i].iconLower.gameObject.SetActive(false);
                radIcons[i].icon.gameObject.SetActive(false);
                radIcons[i].currentIcon = radIcons[i].iconHigher;
            }
            // check for gameobject below the player
            else if (radarPos.y < (playerPos.y - offsetY))
            {

                radIcons[i].iconHigher.gameObject.SetActive(false);
                radIcons[i].iconLower.gameObject.SetActive(true);
                radIcons[i].icon.gameObject.SetActive(false);
                radIcons[i].currentIcon = radIcons[i].iconLower;
            }
            // in the middle
            else
            {
                radIcons[i].iconHigher.gameObject.SetActive(false);
                radIcons[i].iconLower.gameObject.SetActive(false);
                radIcons[i].icon.gameObject.SetActive(true);
                radIcons[i].currentIcon = radIcons[i].icon;
            }
        }

    }





}
