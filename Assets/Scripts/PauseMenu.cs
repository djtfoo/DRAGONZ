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
        //CreateMatchResponse createMatchResponse = new CreateMatchResponse();

        //if (PlayerSetup.isHost)
        //{
        //    //networkManager.matchMaker.DestroyMatch(createMatchResponse.networkId, NetworkManager);
        //}
        //else
        //{
        //    DropConnectionRequest dropReq = new DropConnectionRequest();
        //}

        


        //MatchInfo matchInfo = networkManager.matchInfo;
        //networkManager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, OnConnectionDropped);
        networkManager.StopHost();
    }

    public void OnConnectionDropped(BasicResponse callback)
    {
        Debug.Log("Connection has been dropped on matchmaker server");
        NetworkTransport.Shutdown();
        //m_HostId = -1;
        //m_ConnectionIds.Clear();
        //m_MatchInfo = null;
        //m_MatchCreated = false;
        //m_MatchJoined = false;
        //m_ConnectionEstablished = false;
    }
}
