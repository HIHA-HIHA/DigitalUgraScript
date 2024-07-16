using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MPPlayer = Photon.Realtime.Player;

public class DatasetCountPlayers : MonoBehaviour
{
    [SerializeField]
    private int countPlayers;

    [SerializeField]
    private List<MPPlayer> players;

    public static DatasetCountPlayers Instanse;

    public bool IsMultiplayer { get; set; } = false;

    public int CountPlayers { get => countPlayers; set => countPlayers = value; }
    public List<MPPlayer> Players { get => players; set => players = value; }

    private void Awake()
    {
        if(players == null)
            players = new();

        DontDestroyOnLoad(this);

        if (Instanse)
            Destroy(gameObject);
        else
        {
            Instanse = this;
        }
    }

    public void AddPlayer(MPPlayer newPlayer)
    {
        players.Add(newPlayer);
        Debug.Log("ADD Player");
    }

    public void RemovePlayer(MPPlayer otherPlayer)
    {
        players.Remove(otherPlayer);
        Debug.Log("Remove Player");
    }
}
