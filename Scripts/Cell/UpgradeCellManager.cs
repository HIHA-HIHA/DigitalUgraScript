using Photon.Pun;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Hash = ExitGames.Client.Photon.Hashtable;

public class UpgradeCellManager : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    [SerializeField]
    private GameObject buttonUpgrade;

    private bool managerActive;

    private Cell activeCell;

    private Player activePlayer;


    private void Update()
    {

        if (managerActive && Input.GetMouseButtonDown(0))
        {
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo) && hitInfo.collider.TryGetComponent<Cell>(out var cell) && cell.InStreet && cell.Owner == activePlayer)
            {
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
        buttonUpgrade.SetActive(value);
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

    public void UpgradeCell()
    {
        if (activeCell == null)
            return;

        if (DatasetCountPlayers.Instanse.IsMultiplayer && !activePlayer.PlayerLink.IsLocal)
            return;

        if (activePlayer.Money > activeCell.PriceForUpgrade && activeCell.Owner == activePlayer )
        {
            activePlayer.Money -= activeCell.PriceForUpgrade;
            activeCell.Upgrade();
            if (DatasetCountPlayers.Instanse.IsMultiplayer)
            {
                Hash hash = new();
                hash.Add("UpgradeCell", null);
                hash.Add("cellForUpgrade", SingletonDataset.Instanse.Cells.IndexOf(activeCell));
                PhotonNetwork.SetPlayerCustomProperties(hash);
            }
            TurnButtonView(false);
        }
    }

    public void SetActivePlayer(Player player)
    {
        activePlayer = player;
        TurnState(false);
        TurnButtonView(false);

    }
}
