using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ConnectServer();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("<color=green>Connected To Server</color>");
        base.OnConnectedToMaster();
        string roomName = "Room 1";
        RoomOptions roomOptions = new()
        {
            MaxPlayers = 2,
            IsOpen = true,
            IsVisible = true
        };

        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);

    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined a Room");
    }

    void ConnectServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try Connecting to Server...");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("New Player Joined Room ");// + newPlayer.NickName);
    }
}
