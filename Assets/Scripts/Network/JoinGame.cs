using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class JoinGame : MonoBehaviour {

    List<GameObject> roomList = new List<GameObject>();

    public Text status;

    [SerializeField]
    private GameObject roomListItemPrefab;

    public Transform roomListParent;

    void Start()
    {
        if (NetworkManager.singleton.matchMaker == null)
        {
            NetworkManager.singleton.StartMatchMaker();
        }

        RefreshRoomList();
    }

    public void RefreshRoomList()
    {
        ClearRoomList();
        NetworkManager.singleton.matchMaker.ListMatches(0, 20, "", OnMatchList);
        status.text = "Loading...";
    }

    public void OnMatchList(ListMatchResponse matchList)
    {
        status.text = "";

        if (matchList == null)
        {
            status.text = "No rooms found";
            return;
        }

        foreach (MatchDesc match in matchList.matches)
        {
            GameObject _roomListItemGO = Instantiate(roomListItemPrefab);
            _roomListItemGO.transform.SetParent(roomListParent);

            RoomListItem _roomListItem = _roomListItemGO.GetComponent<RoomListItem>();
            if (_roomListItem != null)
            {
                _roomListItem.Setup(match, JoinRoom);
            }

            roomList.Add(_roomListItemGO);
        }

        if (roomList.Count == 0)
        {
            status.text = "0 rooms";
        }
    }

    void ClearRoomList()
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            Destroy(roomList[i]);
        }

        roomList.Clear();
    }

    public void JoinRoom(MatchDesc _match)
    {
        NetworkManager.singleton.matchMaker.JoinMatch(_match.networkId, "", NetworkManager.singleton.OnMatchJoined);
        ClearRoomList();
        status.text = "Joining Room";
    }
}
