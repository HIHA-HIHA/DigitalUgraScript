using Photon.Pun;

using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Events;

using Hash = ExitGames.Client.Photon.Hashtable;

public class QueueManager : MonoBehaviour
{
    [SerializeField]
    private List<Player> players;


    [SerializeField]
    private Player nowPlayer;

    [SerializeField]
    private UnityEvent<Player> onChangePlayer;


    public Player NowPlayer { get => nowPlayer; private set => nowPlayer = value; }
    public List<Player> Players { get => players; private set => players = value; }

    public void TurnQueue()
    {
        if (DatasetCountPlayers.Instanse.IsMultiplayer)
        {
            if (NowPlayer && !NowPlayer.PlayerLink.IsLocal)
                return;
        }

        if (!NowPlayer)
        {
            NowPlayer = Players[Random.Range(0, Players.Count)];

            if (DatasetCountPlayers.Instanse.IsMultiplayer)
            {
                Hash hash = new();
                hash.Add("activePlayer", NowPlayer.IdPlayer);
                PhotonNetwork.SetPlayerCustomProperties(hash);
            }
            onChangePlayer?.Invoke(NowPlayer);
            return;
        }



        if(NowPlayer.IdPlayer >= Players.Count-1)
        {
            NowPlayer = Players[0];
        }
        else
        {
            NowPlayer = Players[NowPlayer.IdPlayer + 1];
        }


        if (NowPlayer.InPrison)
        {
            NowPlayer.DaysInPrison--;

            if (NowPlayer.DaysInPrison <= 0)
                NowPlayer.InPrison = false;
            TurnQueue();
            return;
        }
        else
        {
            if (DatasetCountPlayers.Instanse.IsMultiplayer)
            {
                Hash hash = new();
                hash.Add("activePlayer", NowPlayer.IdPlayer);
                PhotonNetwork.SetPlayerCustomProperties(hash);
            }

            onChangePlayer?.Invoke(NowPlayer);
        }
    }

    public void ChangeActivePlayer(int newActivePlayerID)
    {
        Debug.Log("Change AP: " + newActivePlayerID);
        players.ForEach(player =>
        {
            if (player.IdPlayer == newActivePlayerID)
                NowPlayer = player;
        } );

        if (NowPlayer.InPrison)
        {
            NowPlayer.DaysInPrison--;
            if (NowPlayer.DaysInPrison <= 0)
                NowPlayer.InPrison = false;
        }

        onChangePlayer?.Invoke(NowPlayer);
    }

    public void AddPlayer(Player newPlayer)
    {
        Players.Add(newPlayer);
    }
}
