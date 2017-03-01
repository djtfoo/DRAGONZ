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
    public Image WO_icon;
    public Image WO_iconHigher;
    public Image WO_iconLower;

    public Image DRAGON_icon;
    public Image DRAGON_iconHigher;
    public Image DRAGON_iconLower;

    public GameObject player;
    private Player playerScript;
    float mapScale = 0.03f;
    int offsetY = 100;
    public static List<GameObject> worldObject = new List<GameObject>();
    public static List<RadarIcon> radIcons = new List<RadarIcon>();
    //public static List<Player> players = new List<Player>();

    private int lengthPlayerGo = 0;

    void RegisterRadarObject(Image icon ,Image iconHigher, Image iconLower)
    {
        Image middleIcon = Instantiate(icon);
        Image higherIcon = Instantiate(iconHigher);
        Image lowerIcon = Instantiate(iconLower);
        // Add into the radar icon list
        radIcons.Add(new RadarIcon() { icon = middleIcon, iconHigher = higherIcon , iconLower=lowerIcon, currentIcon = middleIcon });
    }

    private void SetPlayer(GameObject setPlayer)
    {
        this.player = setPlayer;
    }

    void addPlayerToRadar()
    {
        GameObject[] playersGO = GameObject.FindGameObjectsWithTag("Player");
        bool playerExistsAlready = false;

        for (int i = 0; i < playersGO.Length; ++i)
        {
            if (playersGO[i].layer == 9)  // remoteplayer layer
            {
                for (int j = 0; j < worldObject.Count; ++j)
                {
                    if (worldObject[j].tag == "Player")
                    {
                        if (playersGO[i].name == worldObject[j].name)
                        {
                            playerExistsAlready = true;
                            break;
                        }
                    }
                }

                if (!playerExistsAlready)
                {
                    worldObject.Add(playersGO[i]);
                    RegisterRadarObject(DRAGON_icon, DRAGON_iconHigher, DRAGON_iconLower);
                }
                playerExistsAlready = false;
            }
        }

    }

    public static void RemoveRadarObject(GameObject go)
    {
        // loop through world objects array
        for (int i = 0; i < worldObject.Count; ++i)
        {
            if (worldObject[i] == go)
            {
                Destroy(worldObject[i]);
                worldObject.RemoveAt(i);
                radIcons[i].DestroySelf();
                radIcons.RemoveAt(i);
                break;
            }
        }
    }

    public void ZoomIn()
    {
        if (mapScale > 0.02f)
            mapScale -= 0.005f;
    }

    public void ZoomOut()
    {
        if (mapScale < 0.05f)
            mapScale += 0.005f;
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
            if (worldObjArr[i].GetComponent<WorldObject>().IsLandmark())
            {
                worldObject.Add(worldObjArr[i]);
                RegisterRadarObject(WO_icon, WO_iconHigher, WO_iconLower);
            }
        }


        // find players & set
        GameObject[] playersGO = GameObject.FindGameObjectsWithTag("Player");
        lengthPlayerGo = playersGO.Length;
        for (int i = 0; i < playersGO.Length; ++i)
        {
            if (playersGO[i].layer == 8)    // localplayer layer
            {
                this.SetPlayer(playersGO[i]);
                playerScript = playersGO[i].GetComponent<Player>();
            }
            else if (playersGO[i].layer == 9)  // RemotePlayer layer
            {
                worldObject.Add(playersGO[i]);
                RegisterRadarObject(DRAGON_icon, DRAGON_iconHigher, DRAGON_iconLower);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (!playerScript.isLocalPlayer)
            return;

        // find remoteplayers & add
        if(lengthPlayerGo!= GameObject.FindGameObjectsWithTag("Player").Length)
        {
            addPlayerToRadar();
            ++lengthPlayerGo;
        }

        DrawRadarDots();

        for (int i = 0; i < worldObject.Count; ++i)
        {
            // the position of all the gameobjects on the map
            Vector3 radarPos = worldObject[i].transform.position;
            Vector3 worldObjectScale = worldObject[i].transform.localScale;

            // the position of players
            Vector3 playerPos = PlayerUI.playerPos;//playerScript.transform.position;
      

            // check for gameobject above the player
            if (radarPos.y - worldObjectScale.y > (playerPos.y + offsetY))
            {

                radIcons[i].iconHigher.gameObject.SetActive(true);
                radIcons[i].iconLower.gameObject.SetActive(false);
                radIcons[i].icon.gameObject.SetActive(false);
                radIcons[i].currentIcon = radIcons[i].iconHigher;
            }
            // check for gameobject below the player
            else if (radarPos.y + worldObjectScale.y < (playerPos.y - offsetY))
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

#if !UNITY_ANDROID
        // zoom in/out
        if (Input.GetKeyDown(KeyBoardBindings.GetZoomInKey()))
        {
            this.ZoomIn();
        }
        else if (Input.GetKeyDown(KeyBoardBindings.GetZoomOutKey()))
        {
            this.ZoomOut();
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)   // mouse scroll
        {

            mapScale += 0.01f * Input.GetAxis("Mouse ScrollWheel");
            //if (mapScale < 0.02f)
            //    mapScale = 0.002f;
            //
            //if (mapScale > 0.05f)
            //    mapScale = 0.005f;
        }
#endif
    }   // end of Update()

    // Getters
    public float GetMapScale()
    {
        return mapScale;
    }

    public Vector3 GetPlayerRotation()
    {
        foreach (Transform child in player.transform)
        {
            ThirdPersonCamera camera = child.gameObject.GetComponent<ThirdPersonCamera>();
            if (camera != null)
            {
                return camera.transform.eulerAngles;
            }
        }

        return Vector3.forward; // default
    }


}
