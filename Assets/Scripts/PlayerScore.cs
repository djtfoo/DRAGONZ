using UnityEngine;
using System.Collections;

public class PlayerScore : MonoBehaviour {

    Player player;

	// Use this for initialization
	void Start () 
    {
        player = GetComponent<Player>();
        StartCoroutine(SyncScoreLoop());
	}

    void OnDestroy()
    {
        if (player != null)
            SyncNow();
    }

    IEnumerator SyncScoreLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            SyncNow();
        }
    }

    void SyncNow()
    {
        if (UserAccountManager.IsLoggedIn)
            UserAccountManager.instance.GetData(OnReceivedData);
    }

    void OnReceivedData(string data)
    {
        if (player.GetKills() == 0 && player.GetDeaths() == 0)
            return;

        int kills = DataTranslator.DataToKills(data);
        int deaths = DataTranslator.DataToDeaths(data);

        int newKills = player.GetKills() + kills;
        int newDeaths = player.GetDeaths() + deaths;

        string newData = DataTranslator.ValueToData(newKills, newDeaths);

        Debug.Log("Syncing: " + newData);

        player.SetKills(0);
        player.SetDeaths(0);

        UserAccountManager.instance.SendData(newData);
    }
}
