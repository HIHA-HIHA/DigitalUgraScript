using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;


using Hash = ExitGames.Client.Photon.Hashtable;

public class MPPUNLauncher : MonoBehaviourPunCallbacks
{
    private string nameRoom;

    private string passwordRoom;

    private int maxCountPlayers;

    private List<PlayerRoom> players;

    [SerializeField]
    private PlayerRoom playerRoomPrefab;

    [SerializeField]
    private Transform parentPlayerRoom;

    [SerializeField]
    private GameObject buttonStart;

    [SerializeField]
    private ClientState clientState;

    [SerializeField]
    private TMP_Text nameRoomText;

    public ClientState ClientState { get => clientState; private set => clientState = value; }

    private void Update()
    {
        clientState = PhotonNetwork.NetworkClientState;
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;

        players = new List<PlayerRoom>();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom()
    {
        DatasetCountPlayers.Instanse.IsMultiplayer = true;
        nameRoomText.text = PhotonNetwork.CurrentRoom.Name;
        buttonStart.SetActive(PhotonNetwork.IsMasterClient);
        var players = PhotonNetwork.PlayerList;
        foreach (var player in players)
        {
            CreatePlayer(player);
            DatasetCountPlayers.Instanse.AddPlayer(player);
        }
        PageManager.Instanse.OpenPage("MRoom");
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        CreatePlayer(newPlayer);
        DatasetCountPlayers.Instanse.AddPlayer(newPlayer);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        PlayerRoom deletedPlayer = null;
        players.ForEach(player =>
        {
            if (player.PlayerLink == otherPlayer)
            {
                Destroy(player.gameObject);
                deletedPlayer = player;
            }
        });
        players.Remove(deletedPlayer); 
        
        DatasetCountPlayers.Instanse.RemovePlayer(otherPlayer);
    }

    private void CreatePlayer(Photon.Realtime.Player newPlayer)
    {
        var createdPlayer = Instantiate(playerRoomPrefab, parentPlayerRoom);
        createdPlayer.Setup(newPlayer.NickName, newPlayer);
        players.Add(createdPlayer);
    }

    public void LeaveFromRoom()
    {
        PhotonNetwork.LeaveRoom();
        players.ForEach(player =>
        {
            Destroy(player.gameObject);
        });
        players.Clear();
        DatasetCountPlayers.Instanse.IsMultiplayer = false;
    }

    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(nameRoom))
        {
            Hash roomCustomProps = new Hash();
            roomCustomProps.Add("password", passwordRoom);

            RoomOptions roomOptions = new RoomOptions();

            roomOptions.CustomRoomProperties = roomCustomProps;
            roomOptions.MaxPlayers = DatasetCountPlayers.Instanse.CountPlayers;
            roomOptions.CustomRoomPropertiesForLobby = new string[]
            {
                "password"
            };
            PhotonNetwork.CreateRoom(nameRoom, roomOptions);
        }

    }

    public void SetNameRoom(string newName)
    {
        if (string.IsNullOrEmpty(newName) || newName == null)
        {
            nameRoom = "Room" + Random.Range(100, 1000000);
        }
        else
            nameRoom = newName;
    }

    public void SetPasswordRoom(string password = "")
    {
        passwordRoom = password;
    }
}
