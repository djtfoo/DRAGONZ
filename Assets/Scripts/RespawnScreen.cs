using UnityEngine;
using UnityEngine.UI;

public class RespawnScreen : MonoBehaviour {

    public Text killerText;
    public Text RespawnTimerText;

    public void SetKillerText(string _killer)
    {
        killerText.text = _killer;
    }

    public void SetRespawnTimerText(string _timer)
    {
        RespawnTimerText.text = _timer;
    }
}
