using UnityEngine;
using System.Collections;

public class Scoreboard : MonoBehaviour 
{
    [SerializeField]
    GameObject playerScoreboardItem;

    [SerializeField]
    Transform playerScoreboardList;

    GameObject[] playersGO;

	void OnEnable()
    {
        playersGO = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in playersGO)
        {
            GameObject itemGO = (GameObject)Instantiate(playerScoreboardItem);
            itemGO.transform.SetParent(playerScoreboardList);

            PlayerScoreboardItem item = itemGO.GetComponent<PlayerScoreboardItem>();
            if (item != null)
            {
                item.Setup(player.GetComponent<Player>().username, player.GetComponent<Player>().GetKills(), player.GetComponent<Player>().GetDeaths(), player.GetComponent<Player>().GetBestCombo());
            }
        }
    }

    void OnDisable()
    {
        foreach (Transform child in playerScoreboardList)
        {
            Destroy(child.gameObject);
        }
    }
}
