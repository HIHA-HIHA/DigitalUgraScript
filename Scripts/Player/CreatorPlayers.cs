using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


using Hash = ExitGames.Client.Photon.Hashtable;


public class CreatorPlayers : MonoBehaviour
{
    [SerializeField]
    private Player prefabPlayer;

    [SerializeField]
    private Transform originForSpawnPlayer;

    [SerializeField]
    private int countPlayers;

    [SerializeField]
    private UnityEvent<Player> onCreatePlayer;

    [SerializeField]
    private UnityEvent<List<Player>> onEndCreatePlayers;

    private List<Player> createdPlayers;

    [SerializeField]
    private bool createMPPlayers;


    private void Awake()
    {
        createdPlayers = new List<Player>();
    }

    private void Start()
    {
        if (!PhotonNetwork.IsMasterClient && createMPPlayers)
            return;

        if (!createMPPlayers)
        {
            for (int i = 0; i < DatasetCountPlayers.Instanse.CountPlayers; i++)
            {
                var player = Instantiate(prefabPlayer, originForSpawnPlayer);
                player.IdPlayer = i;
                player.Money = 1500;
                player.transform.parent = null;
                player.Name = "Игрок " + i;
                player.ColorPlayer = GetRandomColor();
                player.TurnColor();

                onCreatePlayer?.Invoke(player);
                createdPlayers.Add(player);

            }
            onEndCreatePlayers?.Invoke(createdPlayers);
        }
        else
        {
            for (int i = 0; i < DatasetCountPlayers.Instanse.CountPlayers; i++)
            {
                var mpPlayer = DatasetCountPlayers.Instanse.Players[i];

                var player = Instantiate(prefabPlayer, originForSpawnPlayer);
                player.IdPlayer = i;
                player.transform.parent = null;
                player.Name = mpPlayer.NickName;
                player.ColorPlayer = GetRandomColor();
                player.TurnColor();
                player.PlayerLink = mpPlayer;
                player.Money = 1500;

                onCreatePlayer?.Invoke(player);
                createdPlayers.Add(player);

                SyncPlayer(player);

                SyncData.Instanse.Players.Add(player);
            }

            if (DatasetCountPlayers.Instanse.IsMultiplayer)
            {
                Hash hash = new();
                hash.Add("EndSpawn", null);
                PhotonNetwork.SetPlayerCustomProperties(hash);
            }
            onEndCreatePlayers?.Invoke(createdPlayers);
        }
    }

    private void SyncPlayer(Player player)
    {

        if (DatasetCountPlayers.Instanse.IsMultiplayer)
        {
            Hash hash = new Hash();
            hash.Add("CreatePlayer", null);
            hash.Add("playerID", player.IdPlayer);
            hash.Add("playerName", player.Name);
            hash.Add("playerColorR", player.ColorPlayer.r);
            hash.Add("playerColorG", player.ColorPlayer.g);
            hash.Add("playerColorB", player.ColorPlayer.b);
            hash.Add("playerColorA", player.ColorPlayer.a);
            hash.Add("playerLink", player.PlayerLink);
            hash.Add("playerMoney", player.Money);
            PhotonNetwork.SetPlayerCustomProperties(hash);
        }
    }

    public void CreatePlayer(Hash hashPlayer)
    {
        var player = Instantiate(prefabPlayer, originForSpawnPlayer);
        player.IdPlayer = (int)hashPlayer["playerID"];
        player.transform.parent = null;
        player.Name = (string)hashPlayer["playerName"];
        player.ColorPlayer = new Color((float)hashPlayer["playerColorR"], (float)hashPlayer["playerColorG"], (float)hashPlayer["playerColorB"], (float)hashPlayer["playerColorA"]);
        player.PlayerLink = (Photon.Realtime.Player)hashPlayer["playerLink"];
        player.Money = (int)hashPlayer["playerMoney"];
        player.TurnColor();
        onCreatePlayer?.Invoke(player);
        createdPlayers.Add(player);
        SyncData.Instanse.Players.Add(player);
    }

    public void EndSpawn()
    {
        onEndCreatePlayers?.Invoke(createdPlayers);
    }

    private Color GetRandomColor()
    {
        var rValue = Random.Range(0f, 1f);
        var gValue = Random.Range(0f, 1f);
        var bValue = Random.Range(0f, 1f);

        return new Color(rValue, gValue, bValue);
    }
}
