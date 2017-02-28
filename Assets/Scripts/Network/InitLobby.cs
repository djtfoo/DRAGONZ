using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InitLobby : MonoBehaviour {

    public enum Type
    {
        ToggleLAN,
        ToggleOnline,
        CreateRoomButton,
        JoinLANButton,
        StatusText,
        ErrorText,
    }

    public Type type;

	// Use this for initialization
	void Start () 
    {
        switch (type)
        {
            case Type.ToggleLAN:
                NetworkManager.singleton.GetComponent<HostGame>().toggleLAN = this.GetComponent<Toggle>();
                break;
            case Type.ToggleOnline:
                NetworkManager.singleton.GetComponent<HostGame>().toggleOnline = this.GetComponent<Toggle>();
                break;
            case Type.CreateRoomButton:
                //this.GetComponent<Button>().onClick.AddListener(delegate { OnClickCreateRoomButton(); });
                //this.GetComponent<Button>().onClick.AddListener(() => NetworkManager.singleton.GetComponent<HostGame>().CreateRoom());
                break;
            case Type.JoinLANButton:
                //this.GetComponent<Button>().onClick.AddListener(delegate { OnClickJoinLANButton(); });
                //this.GetComponent<Button>().onClick.AddListener(() => NetworkManager.singleton.GetComponent<HostGame>().JoinLANRoom());
                break;
            case Type.StatusText:
                NetworkManager.singleton.GetComponent<JoinGame>().status = this.GetComponent<Text>();
                break;
            case Type.ErrorText:
                NetworkManager.singleton.GetComponent<HostGame>().errorText = this.GetComponent<Text>();
                break;
        }
        
	}

    void OnClickCreateRoomButton()
    {
        NetworkManager.singleton.GetComponent<HostGame>().CreateRoom();
    }

    void OnClickJoinLANButton()
    {
        NetworkManager.singleton.GetComponent<HostGame>().JoinLANRoom();
    }
}
