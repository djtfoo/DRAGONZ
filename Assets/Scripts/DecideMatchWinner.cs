using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class DecideMatchWinner : MonoBehaviour {

    private GameObject player;

    private GameObject[] playersGO;
    private GameObject matchWinner;

    public Text matchWinnerText;

	// Use this for initialization
	void Start () 
    {
        player = NetworkManager.singleton.playerPrefab;

        OverlayActive.SetOverlayActive(true);

        // Stop rendering stuff
        //player.GetComponent<Renderer>().enabled = false;

        //GameObject.Find("skydome").SetActive(false);
        //GameObject.Find("MapGenerator").SetActive(false);
        //GameObject.Find("Terrain").SetActive(false);
        //GameObject.Find("WorldSpawner").SetActive(false);
        //GameObject.Find("HealthBarManager").SetActive(false);

        // Set winner/loser text
        playersGO = GameObject.FindGameObjectsWithTag("Player");
        if (playersGO.Length > 1)
        {
            for (int i = 0; i < playersGO.Length - 1; ++i)
            {
                if (playersGO[i].GetComponent<Player>().GetKills() > playersGO[i + 1].GetComponent<Player>().GetKills())
                    matchWinner = playersGO[i];
                else if (playersGO[i].GetComponent<Player>().GetKills() < playersGO[i + 1].GetComponent<Player>().GetKills())
                    matchWinner = playersGO[i + 1];
                else
                    matchWinner = null;
            }
        }
        else
        {
            matchWinner = player;
        }

        if (matchWinner == player)
            matchWinnerText.text = "Winner";
        else if (matchWinner != null)
            matchWinnerText.text = "Loser";
        else
            matchWinnerText.text = "Neutral";
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    public void OnClickReturnToLobbyButton()
    {
        NetworkManager.singleton.StopClient();
        NetworkManager.singleton.StopHost();
        NetworkManager.singleton.StopMatchMaker();
        Network.Disconnect();

        this.gameObject.SetActive(false);
        SceneManager.LoadScene("Lobby");
    }
}
