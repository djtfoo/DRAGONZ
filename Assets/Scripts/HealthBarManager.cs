using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class HealthBarAboveEnemy // For tying player to their own healthbar image
{
    public Health PlayerHealthScript;
    public Image HealthBarAbovePlayer;// MaxHealthBar will be attached as a child to this
}
public class HealthBarManager : NetworkBehaviour {
  
    GameObject MainPlayer;
    public Image HealthBar;
    Camera camera;
    public int amtOfPlayers;
    [SerializeField]
    List<HealthBarAboveEnemy> HealthBarEnemyList = new List<HealthBarAboveEnemy>();

    bool EnemyNotInsideList;
    GameObject TempEnemy;

    void Start()
    {
        EnemyNotInsideList = false;
        TempEnemy = null;

        foreach (GameObject playerGO in GameObject.FindGameObjectsWithTag("Player"))    
        {   
            if (playerGO.layer==9)
            {
                HealthBarAboveEnemy temp = new HealthBarAboveEnemy();
                temp.PlayerHealthScript = playerGO.GetComponent<Health>();
              
                temp.HealthBarAbovePlayer = Instantiate(HealthBar);
                temp.HealthBarAbovePlayer.transform.GetChild(1).GetComponent<Text>().text = playerGO.GetComponent<Player>().username;
                //GameObject.Find("PlayerUI").transform
                temp.HealthBarAbovePlayer.transform.SetParent(MainPlayer.GetComponent<PlayerSetup>().GetPlayerUI().transform);
                HealthBarEnemyList.Add(temp);
            }
            else if(playerGO.layer==8)// Main Player
            {
                MainPlayer = playerGO;
                camera = MainPlayer.GetComponentInChildren<Camera>();
            }
            
        }
        amtOfPlayers = HealthBarEnemyList.Count;
    }

    void CheckEnemyList()
    {
        //bool EnemyNotInsideList = false;
        //GameObject TempEnemy=null;

        if (amtOfPlayers == 0)
        {
            Debug.Log("AmtPlayers==0");
            foreach (GameObject playerGO in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (playerGO.layer == 9)
                {
                    HealthBarAboveEnemy temp = new HealthBarAboveEnemy();
                    temp.PlayerHealthScript = playerGO.GetComponent<Health>();
                    temp.HealthBarAbovePlayer = Instantiate(HealthBar);
                    temp.HealthBarAbovePlayer.transform.GetChild(1).GetComponent<Text>().text = playerGO.GetComponent<Player>().username;
                    Debug.Log(playerGO.GetComponent<Player>().username);
                    temp.HealthBarAbovePlayer.transform.SetParent(MainPlayer.GetComponent<PlayerSetup>().GetPlayerUI().transform);
                    HealthBarEnemyList.Add(temp);
                }
                else if (playerGO.layer == 8)// Main Player
                {
                    MainPlayer = playerGO;
                    camera = MainPlayer.GetComponentInChildren<Camera>();
                }

            }
            amtOfPlayers = HealthBarEnemyList.Count;
        }
        else
        {
            foreach (GameObject playerGO in GameObject.FindGameObjectsWithTag("Player"))
            {
                foreach (HealthBarAboveEnemy temp in HealthBarEnemyList)
                {
                    if (playerGO.layer == 9)
                    {
                        if (temp.PlayerHealthScript.gameObject.name == playerGO.name) // Check whether player is not the same
                        {
                            EnemyNotInsideList = false;
                            TempEnemy = null;
                            break;
                        }
                        else
                        {
                            EnemyNotInsideList = true;
                            TempEnemy = playerGO;
                        }
                    }

                    if (EnemyNotInsideList)
                    {
                        Debug.Log("YES");
                        HealthBarAboveEnemy TempHealthBar = new HealthBarAboveEnemy();
                        TempHealthBar.PlayerHealthScript = TempEnemy.GetComponent<Health>();
                        TempHealthBar.HealthBarAbovePlayer = Instantiate(HealthBar);
                        TempHealthBar.HealthBarAbovePlayer.transform.GetChild(1).GetComponent<Text>().text = TempEnemy.GetComponent<Player>().username;
                        Debug.Log(TempEnemy.GetComponent<Player>().username);
                        TempHealthBar.HealthBarAbovePlayer.transform.SetParent(MainPlayer.GetComponent<PlayerSetup>().GetPlayerUI().transform);
                        HealthBarEnemyList.Add(TempHealthBar);
                        amtOfPlayers = HealthBarEnemyList.Count;
                    }
                }

            }
        }
    }

	// Update is called once per frame
	void Update () {

        //Debug.Log(GameObject.FindGameObjectsWithTag("Player").Length);

        //foreach (GameObject playerGO in GameObject.FindGameObjectsWithTag("Player"))
        //{
        //    Debug.Log(playerGO.name + " " + playerGO.GetComponent<Player>().username);
        //}
        if (!MainPlayer.GetComponent<Player>().isLocalPlayer)
            return;
        if (MainPlayer == null) 
        {
            foreach (GameObject playerGO in GameObject.FindGameObjectsWithTag("Player"))
            {

                if (playerGO.layer == 8)
                {
                    MainPlayer = playerGO;
                    camera = MainPlayer.GetComponentInChildren<Camera>();
                    break;
                }
            }
        }
        if (amtOfPlayers != GameObject.FindGameObjectsWithTag("Player").Length - 1) // Minus one cos excluding main player
        {
            Debug.Log("FUCK OFF");
            CheckEnemyList();
        }
        foreach(HealthBarAboveEnemy temp in HealthBarEnemyList)
        {
            if(MainPlayer.GetComponent<Health>().currentHealth <=0)
            {
                temp.HealthBarAbovePlayer.gameObject.SetActive(false);
                continue;
            }
            else
                temp.HealthBarAbovePlayer.gameObject.SetActive(true);

            if (temp.PlayerHealthScript.currentHealth == 0)
                temp.HealthBarAbovePlayer.gameObject.SetActive(false);
            else
                temp.HealthBarAbovePlayer.gameObject.SetActive(true);

            temp.HealthBarAbovePlayer.transform.GetChild(0).GetComponent<Image>().fillAmount = temp.PlayerHealthScript.currentHealth / temp.PlayerHealthScript.MaxHealth;

            if (temp.HealthBarAbovePlayer.transform.parent != MainPlayer.GetComponent<PlayerSetup>().GetPlayerUI().transform)
                temp.HealthBarAbovePlayer.transform.SetParent(MainPlayer.GetComponent<PlayerSetup>().GetPlayerUI().transform);

            temp.HealthBarAbovePlayer.transform.position = camera.WorldToScreenPoint(temp.PlayerHealthScript.gameObject.transform.position + temp.PlayerHealthScript.gameObject.transform.up * 50);
            //temp.HealthBarAbovePlayer.transform.position = new Vector3(temp.HealthBarAbovePlayer.transform.position.x, temp.HealthBarAbovePlayer.transform.position.y, 0f);

            
            Vector3 dist = MainPlayer.transform.position - temp.PlayerHealthScript.gameObject.transform.position;

            //Vector3 playerView = MainPlayer.GetComponent<PlayerMovement>().GetView();
            //float cosAngle = playerView.x * dist.x + playerView.y * dist.y + playerView.z * dist.z;
            //
            //if (cosAngle > 0f)
            //    temp.HealthBarAbovePlayer.transform.position = new Vector3(temp.HealthBarAbovePlayer.transform.position.x, temp.HealthBarAbovePlayer.transform.position.y, -999999f);

            float minDistRange = 2000f; // within this distance, hp bar is at 1x scale (aka largest possible)
            float reducingFactor = 3000f;   // ratio of reduction
            float healthbarScale = Mathf.Clamp((minDistRange - dist.magnitude) / reducingFactor, 0f, 1f);

            temp.HealthBarAbovePlayer.transform.localScale = new Vector3(healthbarScale, healthbarScale, 1f);
        }
        
       
	}
}
