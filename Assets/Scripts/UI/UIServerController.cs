using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIServerController : MonoBehaviourPunCallbacks
{
    [Header("Login")]
    [SerializeField] private GameObject loginContainer = null;
    [SerializeField] private TMP_InputField nickNameField = null;
    [SerializeField] private Button loginBtn = null;

    [Header("Lobby")]
    [SerializeField] private UILobbyController _lobbyController = null;

    [Header("Room")]
    [SerializeField] private UIRoomController _roomController = null;

    private void Start() => Setup();

    private void Setup()
    {
        loginContainer.SetActive(true);
        _lobbyController.Setup();
        _roomController.Setup();

        nickNameField.text = PlayerPrefsController.GetSavedUserNickName();
        nickNameField.onValueChanged.AddListener(SetPlayerBtn);
        SetPlayerBtn(nickNameField.text);
        loginBtn.onClick.AddListener(Login);
    }

    private void SetPlayerBtn(string inputValue)
    {
        loginBtn.interactable = !string.IsNullOrEmpty(inputValue);
    }

    private void Login()
    {
        string user = nickNameField.text;
        PlayerPrefsController.SaveUserNickName(user);
        NetworkManager.Instance.ConnectToServer(user);
    }

    //Override functions
    public override void OnConnectedToMaster()
    {
        Debug.Log("Client connected to master server " + PhotonNetwork.LocalPlayer.NickName);

        //you have to be in a lobby to get the room list update
        PhotonNetwork.JoinLobby(); //uses the default lobby
    }
    public override void OnJoinedLobby()
    {
        Debug.Log($"Client joined a lobby " + PhotonNetwork.CurrentLobby);

        loginContainer.SetActive(false);
        _lobbyController.SetActive(true);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log($"Client joined a room " + PhotonNetwork.CurrentRoom);

        _lobbyController.SetActive(false);
        _roomController.SetActive(true);
    }


}
