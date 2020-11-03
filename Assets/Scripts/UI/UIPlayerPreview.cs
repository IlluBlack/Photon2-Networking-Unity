using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayerPreview : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text = null;

    public Player PlayerInfo { get; private set; }

    public void SetupPlayerInfo(Player data)
    {
        PlayerInfo = data;
        _text.text = data.NickName;
    }
}
