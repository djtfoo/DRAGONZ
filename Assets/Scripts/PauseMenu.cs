using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class PauseMenu : NetworkBehaviour {

    //public static bool isOn = false;

    private NetworkManager networkManager;

    void Start()
    {
        networkManager = NetworkManager.singleton;
    }

    public void LeaveRoom()
    {
        //networkManager.StopHost();

        networkManager.StopClient();
        networkManager.StopHost();
        networkManager.StopMatchMaker();
        Network.Disconnect();
    }
}
