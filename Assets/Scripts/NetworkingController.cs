using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkingController : MonoBehaviourPunCallbacks
{
    public const string GAME_VERSION = "0.1"; //Just players with same version can play together
    public const int MAX_PLAYERS_PER_ROOM = 2;

    private bool isConnecting = false;
    public static bool connected = false;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true; //if master client loads scene, then all load the scene
    }

    public static void SetUserNickName(string nickName)
    {
        PhotonNetwork.NickName = nickName;
        Debug.Log("Client nickname setted to " + nickName);
    }

    public void FindOpponent()
    {
        isConnecting = true;

        if (PhotonNetwork.IsConnected) //the player was already connected, he played, was back on the menu
        {
            Debug.Log("Client was already connected, joining a room");
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.GameVersion = GAME_VERSION;
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Connect client to master");
        }
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Client connected to master");
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
            Debug.Log("Joining a room");
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        //base.OnDisconnected(cause);
        Debug.Log($"Client disconnected due to {cause}");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        //not disconnected but the join random room failed

        Debug.Log("No other clients waiting for an opponent, create a new room");

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MAX_PLAYERS_PER_ROOM });
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        Debug.Log("Client successuflly joined a room");
        if(PhotonNetwork.CurrentRoom.PlayerCount != MAX_PLAYERS_PER_ROOM)
        {
            Debug.Log("Client is waiting for an opponent");
        }
        else
        {
            Debug.Log("Match is ready to begin");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        if (PhotonNetwork.CurrentRoom.PlayerCount == MAX_PLAYERS_PER_ROOM)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            Debug.Log("Room is closed, and match is ready to begin");
        }
    }
}
