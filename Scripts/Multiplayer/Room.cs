using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private PlayerRoom playerRoomPrefab;

    [SerializeField]
    private Transform parentPlayerRoom;

    private List<PlayerRoom> players;


    [SerializeField]
    private Button buttonStart;

    private void OnEnable()
    {
        buttonStart.gameObject.SetActive(PhotonNetwork.IsMasterClient);
    }

}
