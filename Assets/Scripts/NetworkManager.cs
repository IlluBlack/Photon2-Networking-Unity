using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Documentation: https://doc-api.photonengine.com/en/pun/v2/class_photon_1_1_pun_1_1_mono_behaviour_pun_callbacks.html

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance;
    public const string GAME_VERSION = "0.1"; //Just players with same version can play together
    public const int MAX_PLAYERS_PER_ROOM = 4;

    public const string KEY_RANDOM_PROPERTY = "RandomNumber";

    private void Awake()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    //connect to master server, see all available rooms
    public void ConnectToServer(string nickName)
    {
        PhotonNetwork.NickName = nickName;
        Debug.Log("Client nickname setted to " + nickName);
        PhotonNetwork.AutomaticallySyncScene = true; //if master client loads scene, then all load the scene

        Debug.Log("Connecting to server");
        PhotonNetwork.GameVersion = GAME_VERSION;
        PhotonNetwork.ConnectUsingSettings();
        //PhotonNetwork.ConnectToBestCloudServer(); //connect to the one with the best ping for user
        //PhotonNetwork.ConnectToMaster(...); //passin server id, port, and app id
        //PhotonNetwork.ConnectToRegion(...); //connect to asia, usa...
    }

    //Override functions
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Client disconnected from server due to {cause}");
    }

    /*public void CreateRoom(string name)
    {
        PhotonNetwork.CreateRoom(name);
    }

    public void JoinRoom(string name)
    {
        PhotonNetwork.JoinRoom(name);
    }*/

    public static void LoadScene(int idx)
    {
        if (PhotonNetwork.IsMasterClient) {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            //PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(idx);
        }
    }

}
