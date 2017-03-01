using UnityEngine;
using System.Collections;

public class PlayerScore : MonoBehaviour {

    int lastKills = 0;
    int lastDeaths = 0;

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
        if (player.GetKills() <= lastKills && player.GetDeaths() <= lastDeaths)
            return;

        int killsSince = player.GetKills() - lastKills;
        int deathsSince = player.GetDeaths() - lastDeaths;

        if (killsSince == 0 && deathsSince == 0)
            return;

        int kills = DataTranslator.DataToKills(data);
        int deaths = DataTranslator.DataToDeaths(data);

        int newKills = killsSince + kills;
        int newDeaths = deathsSince + deaths;

        string newData = DataTranslator.ValueToData(newKills, newDeaths);

        //Debug.Log("Syncing: " + newData);

        lastKills = player.GetKills();
        lastDeaths = player.GetDeaths();

        UserAccountManager.instance.SendData(newData);
    }
}
