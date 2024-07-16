using Photon.Pun;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Hash = ExitGames.Client.Photon.Hashtable;

public class SellCellManager : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    [SerializeField]
    private GameObject buttonSell;

    private bool managerActive;

    private Cell activeCell;

    private Player activePlayer;


    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo) && hitInfo.collider.TryGetComponent<Cell>(out var cell) && cell.Owner == activePlayer)
            {
                if (DatasetCountPlayers.Instanse.IsMultiplayer && !cell.Owner.PlayerLink.IsLocal)
                    return;

                TurnButtonView(true);
                activeCell = cell;
            }
            else
            {
                if (hitInfo.collider == null)
                    return;

                Debug.Log("RAY: " + hitInfo.collider.name);
                activeCell = null;
                TurnButtonView(false);
            }
        }

    }

    private void TurnButtonView(bool value)
    {
        buttonSell.SetActive(value);
    }


    public void TurnState()
    {
        managerActive = !managerActive;
        activeCell = null;
        
    }

    public void TurnState(bool newState)
    {
        managerActive = newState;
        activeCell = null;
        
    }

    public void SellCell()
    {
        activePlayer.Money += activeCell.PriceForBuy;
        activeCell.Sell();
        activePlayer.RemoveBuyedCell(activeCell);
        if (DatasetCountPlayers.Instanse.IsMultiplayer)
        {
            Hash hash = new();
            hash.Add("SellCell", null);
            hash.Add("cellForSell", SingletonDataset.Instanse.Cells.IndexOf(activeCell));
            PhotonNetwork.SetPlayerCustomProperties(hash);
        }
        TurnButtonView(false);
    }

    public void SetActivePlayer(Player player)
    {
        activePlayer = player;
        TurnState(false);
        TurnButtonView(false);

    }
}
