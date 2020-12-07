using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;

public class UIPlayerPreview : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI _text = null;

    public Player PlayerInfo { get; private set; }

    public void SetupPlayerInfo(Player data)
    {
        PlayerInfo = data;
        UpdatePlayerText(data);
    }

    private void UpdatePlayerText(Player data)
    {
        int randomProperty = -1;
        if (data.CustomProperties.ContainsKey(NetworkManager.KEY_RANDOM_PROPERTY))
            randomProperty = (int)data.CustomProperties[NetworkManager.KEY_RANDOM_PROPERTY];

        _text.text = randomProperty.ToString() + ", " + data.NickName;
    }

    //Override
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        if(targetPlayer != null && targetPlayer == PlayerInfo) {
            if(changedProps.ContainsKey(NetworkManager.KEY_RANDOM_PROPERTY))
                UpdatePlayerText(targetPlayer);
        }
    }
}
