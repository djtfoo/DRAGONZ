using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreboardItem : MonoBehaviour {

    [SerializeField]
    Text usernameText;

    [SerializeField]
    Text killsText;

    [SerializeField]
    Text deathsText;

    [SerializeField]
    Text comboText;

    public void Setup(string username, int kills, int deaths, int combo)
    {
        usernameText.text = username;
        killsText.text = kills.ToString();
        deathsText.text = deaths.ToString();
    }
}
