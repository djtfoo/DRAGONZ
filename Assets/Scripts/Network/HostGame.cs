using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class HostGame : NetworkBehaviour {

    [SerializeField]
    private uint roomSize = 16;

    private string roomName;

    private NetworkManager networkManager;

    void Start()
    {
        networkManager = NetworkManager.singleton;

        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }
    }

    public void SetRoomName(string _name)
    {
        roomName = _name;
    }

    public void CreateRoom()
    {
        if (roomName != "" && roomName != null)
        {
            Debug.Log("Creating Room: " + roomName + " Size: " + roomSize);
            PlayerSetup.isHost = true;
            // Create room
            networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", networkManager.OnMatchCreate);
        }
    }
}
