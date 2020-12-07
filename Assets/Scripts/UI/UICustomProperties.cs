using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

public class UICustomProperties : MonoBehaviour
{
    //For testing: Saving random number in properties
    [SerializeField] private TextMeshProUGUI randomPropertyTxt = null;
    [SerializeField] private Button randomBtn = null;

    private ExitGames.Client.Photon.Hashtable _customProperties = new ExitGames.Client.Photon.Hashtable();

    public void Setup()
    {
        randomBtn.onClick.AddListener(SetCustomProperty);
    }

    private void SetCustomProperty()
    {
        System.Random _random = new System.Random();
        int randomNumber = _random.Next(0, 99);

        randomPropertyTxt.text = randomNumber.ToString();

        _customProperties[NetworkManager.KEY_RANDOM_PROPERTY] = randomNumber;
        PhotonNetwork.SetPlayerCustomProperties(_customProperties);
    }
}
