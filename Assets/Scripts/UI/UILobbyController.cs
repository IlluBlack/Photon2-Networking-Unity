using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class UILobbyController : MonoBehaviourPunCallbacks
{
    [Header("Join or create room")]
    [SerializeField] private TMP_InputField roomNameField = null;
    [SerializeField] private Button joinOrCreateRoomBtn = null;

    [Header("Room listing")]
    [SerializeField] private UIRoomPreview templateRoomPreview = null;
    [SerializeField] private Transform parentRoomList = null;
    private List<UIRoomPreview> roomPreviewList = new List<UIRoomPreview>();

    public void SetActive(bool state)
    {
        this.gameObject.SetActive(state);
    }

    public void Setup()
    {
        joinOrCreateRoomBtn.onClick.AddListener(JoinOrCreateRoom);
        SetActive(false);
    }

    private void JoinOrCreateRoom()
    {
        if (PhotonNetwork.IsConnected) //the player is connected
        {
            PhotonNetwork.JoinOrCreateRoom(roomNameField.text,
                new RoomOptions { MaxPlayers = NetworkManager.MAX_PLAYERS_PER_ROOM }, TypedLobby.Default);
        }
        else
        {
            Debug.LogError("You are not connected, login first");
        }
    }

    private void UpdateRoomPreviewList(List<RoomInfo> newRoomList)
    {
        foreach (RoomInfo roomInfo in newRoomList)
        {
            if (roomInfo.RemovedFromList)
            {
                //remove from list
                int idxRemoved = newRoomList.FindIndex(room => room.Name == roomInfo.Name);

                if(idxRemoved != -1)
                {
                    Destroy(roomPreviewList[idxRemoved].gameObject);
                    roomPreviewList.RemoveAt(idxRemoved);
                }
            }
            else
            {
                //add to list
                UIRoomPreview UIPreview = Instantiate(templateRoomPreview, parentRoomList);
                if(UIPreview != null)
                {
                    UIPreview.SetupRoomInfo(roomInfo);
                    roomPreviewList.Add(UIPreview);
                }
            }
        }
    }

    //Override functions
    public override void OnCreatedRoom()
    {
        Debug.Log("Room created: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed " + message);
    }

    public override void OnRoomListUpdate(List<RoomInfo> newRoomList)
    {
        Debug.Log("Room list updated");
        UpdateRoomPreviewList(newRoomList);
    }





   /* private void FindOpponents()
    {
        findOpponentPanel.SetActive(false);
        waitingStatusPanel.SetActive(true);
        waitingStatusTxt.text = "Searching...";

        waiting = true;
    }

    private void OnDisconnected()
    {
        findOpponentPanel.SetActive(true);
        waitingStatusPanel.SetActive(false);
    }*/
}
