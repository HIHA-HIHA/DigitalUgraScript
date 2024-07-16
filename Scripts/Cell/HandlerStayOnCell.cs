using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HandlerStayOnCell : MonoBehaviour
{
    [SerializeField]
    private CardActionOneTypeSupports cardActionOneTypeSupports;

    [SerializeField]
    private CardActionTwoTaxInspection cardActionTwoTaxInspection;

    [SerializeField]
    private CardActionTreeRoskomnadzor cardActionTreeRoskomnadzor;

    [SerializeField]
    private CardActionFour cardActionFour;

    [SerializeField]
    private CardActionFiveTechPark cardActionFiveTechPark;

    [SerializeField]
    private CardActionSixFSTEK cardActionSixFSTEK;

    public void OnStayOnCell(Player player, Cell cell)
    {
        switch (cell.TypeCell)
        {
            case TypeCell.Start:
                player.Money += 500 + player.Bonus;
                player.Bonus = 0;
                break;

            case TypeCell.Default:
                if (cell.IsBuyed && player != cell.Owner)
                {
                    player.Money -= cell.PriceOnStay;
                    cell.Owner.Money += cell.PriceOnStay;
                }
                break;

            case TypeCell.Prison:
                player.InPrison = true;
                player.DaysInPrison = 2;
                SingletonDataset.Instanse.QueueManager.TurnQueue();

                break;
            case TypeCell.Card1:
                cardActionOneTypeSupports.Activate();
                Debug.Log("HSOC: Card1 Action");
                break;
            case TypeCell.Card2:
                cardActionTwoTaxInspection.Activate();
                Debug.Log("HSOC: Card2 Action");
                break;
            case TypeCell.Card3:
                cardActionFiveTechPark.Activate();
                Debug.Log("HSOC: Card5 Action");
                break;
            case TypeCell.Card4:
                cardActionTreeRoskomnadzor.Activate();
                Debug.Log("HSOC: Card3 Action");
                break;
            case TypeCell.Card5:
                cardActionFour.Activate();
                Debug.Log("HSOC: Card4 Action");
                break;
            case TypeCell.Card6:
                cardActionSixFSTEK.Activate();
                Debug.Log("HSOC: Card6 Action");
                break;
            default:
                break;
        }
    }

}
