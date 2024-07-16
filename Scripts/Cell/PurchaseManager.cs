using Photon.Pun;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Hash = ExitGames.Client.Photon.Hashtable;
public class PurchaseManager : MonoBehaviour
{
    private Player activePlayer;
    public void SetActivePlayer(Player nowPlayer)
    {
        activePlayer = nowPlayer;
    }

    public void BuyCell()
    {
        if (activePlayer.NowCell.TypeCell != TypeCell.Default)
            return;

        if (activePlayer.NowCell.IsBuyed)
            return;

        if (DatasetCountPlayers.Instanse.IsMultiplayer && !activePlayer.PlayerLink.IsLocal)
            return;

        if (activePlayer.Money < activePlayer.NowCell.PriceForBuy)
            return;




        activePlayer.Money -= activePlayer.NowCell.PriceForBuy;
        activePlayer.NowCell.SetOwner(activePlayer);
        activePlayer.AddBuyedCell(activePlayer.NowCell);

        if (DatasetCountPlayers.Instanse.IsMultiplayer)
        {
            Hash hash = new Hash();
            hash.Add("link", activePlayer.PlayerLink);
            hash.Add("BuyCell", null);
            hash.Add("idPlayer", activePlayer.IdPlayer);
            hash.Add("cellId", SingletonDataset.Instanse.Cells.IndexOf(activePlayer.NowCell));
            PhotonNetwork.SetPlayerCustomProperties(hash);
        }

    }


    public void BuyCell(Cell cell)
    {
        if (DatasetCountPlayers.Instanse.IsMultiplayer && !activePlayer.PlayerLink.IsLocal)
            return;

        if (activePlayer.Money < cell.PriceForBuy)
            return;

        if (cell.IsBuyed)
            return;

        if (cell.TypeCell != TypeCell.Default)
            return;



        activePlayer.Money -= activePlayer.NowCell.PriceForBuy;
        activePlayer.NowCell.SetOwner(activePlayer);
        activePlayer.AddBuyedCell(activePlayer.NowCell);

        if (DatasetCountPlayers.Instanse.IsMultiplayer)
        {
            Hash hash = new Hash();
            hash.Add("link", activePlayer.PlayerLink);
            hash.Add("BuyCell", null);
            hash.Add("idPlayer", activePlayer.IdPlayer);
            hash.Add("cellId", SingletonDataset.Instanse.Cells.IndexOf(activePlayer.NowCell));
            PhotonNetwork.SetPlayerCustomProperties(hash);
        }

    }

}