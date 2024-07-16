using Photon.Pun;
using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class SyncData : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private List<Player> players;

    public static SyncData Instanse;

    public List<Player> Players { get => players; set => players = value; }

    private void Awake()
    {
        Instanse = this;
        players = new();
    }

    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, Hashtable changedProps)
    {
        if (changedProps.ContainsKey("EndGame") && !PhotonNetwork.IsMasterClient)
        {
            SingletonDataset.Instanse.EndGameObject.OpenEndGamePanel();
        }

            if (changedProps.ContainsKey("CreatePlayer") && !PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Create Players");
            SingletonDataset.Instanse.CreatorPlayers.CreatePlayer(changedProps);
            return;
        }

        if (changedProps.ContainsKey("EndSpawn") && !PhotonNetwork.IsMasterClient)
        {
            Debug.Log("End Spawn Players");
            SingletonDataset.Instanse.CreatorPlayers.EndSpawn();
            return;
        }

        if (changedProps.ContainsKey("activePlayer"))
            SingletonDataset.Instanse.QueueManager.ChangeActivePlayer((int)changedProps["activePlayer"]);

        if (changedProps.ContainsKey("SellCell"))
        {
            SingletonDataset.Instanse.Cells[(int)changedProps["cellForSell"]].Sell();
        }

        if (changedProps.ContainsKey("UpgradeCell"))
        {
            SingletonDataset.Instanse.Cells[(int)changedProps["cellForUpgrade"]].Upgrade();
        }

        players.ForEach(player =>
        {
            if (player.PlayerLink == targetPlayer && player.PlayerLink == (Photon.Realtime.Player)changedProps["link"] && !player.PlayerLink.IsLocal)
            {
                if (changedProps.ContainsKey("money"))
                    player.Money = (int)changedProps["money"];


                if (changedProps.ContainsKey("pos") && changedProps["pos"] != null)
                { 
                    player.NowCell = SingletonDataset.Instanse.Cells[(int)changedProps["pos"]];
                    player.gameObject.transform.position = player.NowCell.GetRandomPointForPlayer();
                }


                if (changedProps.ContainsKey("BuyCell"))
                {
                    if (player.IdPlayer == (int)changedProps["idPlayer"])
                    {
                        player.NowCell.SetOwner(player);
                        player.BuyedCells.Add(player.NowCell);
                    }
                }


            }

        });
    }

}
