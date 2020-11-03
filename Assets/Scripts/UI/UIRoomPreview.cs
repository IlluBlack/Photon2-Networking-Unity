using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRoomPreview : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text = null;
    [SerializeField] private Button joinRoomBtn = null;

    public RoomInfo RoomInfo { get; private set; }

    public void SetupRoomInfo(RoomInfo data)
    {
        RoomInfo = data;
        _text.text = $"{data.MaxPlayers}, {data.Name}";

        joinRoomBtn.onClick.AddListener(JoinRoom);
    }

    private void JoinRoom()
    {
        PhotonNetwork.JoinRoom(RoomInfo.Name);
    }
}
