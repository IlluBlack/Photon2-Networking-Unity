using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class UIRoomController : MonoBehaviourPunCallbacks
{
    [Header("Room")]
    [SerializeField] private TextMeshProUGUI nameRoomTxt = null;
    [SerializeField] private Button leaveRoomBtn = null;

    [Header("Players listing")]
    [SerializeField] private UIPlayerPreview templatePlayerPreview = null;
    [SerializeField] private Transform parentPlayerList = null;
    private List<UIPlayerPreview> playerPreviewList = new List<UIPlayerPreview>();

    public void SetActive(bool state)
    {
        if (state) {
            SetCurrentRoomData();  //update list with the current players in the room
        }

        this.gameObject.SetActive(state);
    }

    private void SetCurrentRoomData()
    {
        Room currentRoom = PhotonNetwork.CurrentRoom;
        nameRoomTxt.text = currentRoom.Name;

        foreach (KeyValuePair<int, Player> playerData in currentRoom.Players)
        {
            AddPlayer(playerData.Value);
        }
    }

    public void Setup()
    {
        leaveRoomBtn.onClick.AddListener(LeaveRoom);
        SetActive(false);
    }

    private void LeaveRoom()
    {

    }

    private void AddPlayer(Player newPlayer)
    {
        UIPlayerPreview UIPreview = Instantiate(templatePlayerPreview, parentPlayerList);
        if (UIPreview != null)
        {
            UIPreview.SetupPlayerInfo(newPlayer);
            playerPreviewList.Add(UIPreview);
        }
    }

    private void RemovePlayer(Player removePlayer)
    {
        int idxRemoved = playerPreviewList.FindIndex(player => player.PlayerInfo == removePlayer);

        if (idxRemoved != -1)
        {
            Destroy(playerPreviewList[idxRemoved].gameObject);
            playerPreviewList.RemoveAt(idxRemoved);
        }
    }


    //Override functions
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player entered the room " + newPlayer.NickName);
        AddPlayer(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player left the room " + otherPlayer.NickName);
        RemovePlayer(otherPlayer);
    }
}
