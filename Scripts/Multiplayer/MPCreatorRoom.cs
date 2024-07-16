using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MPCreatorRoom : MonoBehaviourPunCallbacks
{
    private string nameRoom;

    private int maxCountPlayers;

    [SerializeField]
    private UnityEvent onJoinRoom;

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxCountPlayers;
        PhotonNetwork.CreateRoom(nameRoom, roomOptions);
    }

    public void SetNameRoom(string newName)
    {
        nameRoom = newName;
    }

    public void SetMaxCountPlayers(int maxPlayers)
    {
        maxCountPlayers = maxPlayers + 2;
    }


    public override void OnJoinedRoom()
    {
        onJoinRoom?.Invoke();
    }


}
