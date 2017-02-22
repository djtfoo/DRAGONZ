using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class HostGame : NetworkBehaviour {

    [SerializeField]
    private uint roomSize = 16;

    public Text errorText;

    private string roomName;
    public Toggle toggleLAN;
    public Toggle toggleOnline;
    private bool canCreate;

    private NetworkManager networkManager;

    void Start()
    {
        networkManager = NetworkManager.singleton;
        canCreate = false;
        if (errorText != null)
            errorText.text = "";

        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }
    }

    public void SetRoomName(string _name)
    {
        roomName = _name;
    }

    public void SetLAN()
    {
        //if (!toggleLAN.isOn)
        //    toggleLAN.isOn = true;
        //else
        //    toggleLAN.isOn = false;

        if (toggleOnline.isOn)
            toggleOnline.isOn = false;
    }

    public void SetOnline()
    {
        //if (!toggleOnline.isOn)
        //    toggleOnline.isOn = true;
        //else
        //    toggleOnline.isOn = false;

        if (toggleLAN.isOn)
            toggleLAN.isOn = false;
    }

    public void CreateRoom()
    {
        if (!toggleLAN.isOn && !toggleOnline.isOn)
        {
            canCreate = false;
            errorText.text = "Select game type";
            return;
        }

        if (roomName == "" || roomName == null)
        {
            canCreate = false;
            errorText.text = "Room name cannot be empty";
            return;
        }

        canCreate = true;

        if (canCreate)
        {
            PlayerSetup.isHost = true;

            if (toggleLAN.isOn)
            {
                Debug.Log("Creating LAN Room: " + roomName + " Size: " + roomSize);
                networkManager.StartHost();
            }
            else if (toggleOnline.isOn)
            {
                Debug.Log("Creating online Room: " + roomName + " Size: " + roomSize);
                networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", networkManager.OnMatchCreate);
            }
        }
    }
}
