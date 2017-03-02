using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InitLobby : MonoBehaviour {

    public enum Type
    {
        ToggleLAN,
        ToggleOnline,
        RoomNameInput,
        CreateRoomButton,
        IPAddressInput,
        JoinLANButton,
        RoomListParent,
        RefreshRoomList,
        StatusText,
        CreateRoomErrorText,
        JoinLANText,
    }

    public Type type;

	// Use this for initialization
	void Start () 
    {
        if (NetworkManager.singleton.GetComponent<MatchTimer>().enabled)
            NetworkManager.singleton.GetComponent<MatchTimer>().enabled = false;

        if (NetworkManager.singleton.matchMaker == null)
            NetworkManager.singleton.StartMatchMaker();

        switch (type)
        {
            case Type.ToggleLAN:
                NetworkManager.singleton.GetComponent<HostGame>().toggleLAN = this.GetComponent<Toggle>();
                this.GetComponent<Toggle>().onValueChanged.AddListener(delegate { OnToggleLAN(); });
                break;

            case Type.ToggleOnline:
                NetworkManager.singleton.GetComponent<HostGame>().toggleOnline = this.GetComponent<Toggle>();
                this.GetComponent<Toggle>().onValueChanged.AddListener(delegate { OnToggleOnline(); });
                break;

            case Type.RoomNameInput:
                NetworkManager.singleton.GetComponent<HostGame>().SetRoomName("");
                this.GetComponent<InputField>().onValueChanged.AddListener(delegate { OnSetRoomName(this.GetComponent<InputField>().text); });
                break;

            case Type.CreateRoomButton:
                this.GetComponent<Button>().onClick.AddListener(delegate { OnClickCreateRoomButton(); });
                break;

            case Type.IPAddressInput:
                NetworkManager.singleton.GetComponent<HostGame>().SetIPAddress("");
                this.GetComponent<InputField>().onValueChanged.AddListener(delegate { OnSetIPAddress(this.GetComponent<InputField>().text); });
                break;

            case Type.JoinLANButton:
                this.GetComponent<Button>().onClick.AddListener(delegate { OnClickJoinLANButton(); });
                break;

            case Type.RoomListParent:
                NetworkManager.singleton.GetComponent<JoinGame>().roomListParent = this.GetComponent<Transform>();
                break;

            case Type.RefreshRoomList:
                this.GetComponent<Button>().onClick.AddListener(delegate { OnClickRefreshRoomList(); });
                break;

            case Type.StatusText:
                NetworkManager.singleton.GetComponent<JoinGame>().status = this.GetComponent<Text>();
                break;

            case Type.CreateRoomErrorText:
                NetworkManager.singleton.GetComponent<HostGame>().createRoomErrorText = this.GetComponent<Text>();
                NetworkManager.singleton.GetComponent<HostGame>().createRoomErrorText.text = "";
                break;

            case Type.JoinLANText:
                NetworkManager.singleton.GetComponent<HostGame>().joinLANGameText = this.GetComponent<Text>();
                NetworkManager.singleton.GetComponent<HostGame>().joinLANGameText.text = "";
                break;
        }
        
	}

    void OnToggleLAN()
    {
        NetworkManager.singleton.GetComponent<HostGame>().SetLAN();
    }

    void OnToggleOnline()
    {
        NetworkManager.singleton.GetComponent<HostGame>().SetOnline();
    }

    void OnClickCreateRoomButton()
    {
        NetworkManager.singleton.GetComponent<HostGame>().CreateRoom();
    }

    void OnSetRoomName(string _roomName)
    {
        NetworkManager.singleton.GetComponent<HostGame>().SetRoomName(_roomName);
    }

    void OnSetIPAddress(string _ipAddress)
    {
        NetworkManager.singleton.GetComponent<HostGame>().SetIPAddress(_ipAddress);
    }

    void OnClickJoinLANButton()
    {
        NetworkManager.singleton.GetComponent<HostGame>().JoinLANRoom();
    }

    void OnClickRefreshRoomList()
    {
        NetworkManager.singleton.GetComponent<JoinGame>().RefreshRoomList();
    }
}
