using JetBrains.Annotations;

using Photon.Pun;

using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

public class Street : MonoBehaviour
{
    private int nowCellsInStreet;

    [SerializeField]
    private List<Cell> cells;

    [SerializeField]
    private Color streetColor;

    [SerializeField]
    private string nameStreet;

    [SerializeField]
    private int defaultPriceBuy,
        defaultPriceUpgrade,
        defaultPriceStayOnCell;

    public string NameStreet { get => nameStreet; private set => nameStreet = value; }

    public void AddCell()
    {
        nowCellsInStreet++;
        Player buyedPlayer = null;


        bool streetCompleted = true;
        if(nowCellsInStreet >= cells.Count)
        {
            cells.ForEach(cell =>
            {
                if (buyedPlayer == null)
                    buyedPlayer = cell.Owner;

                if (cell.Owner != buyedPlayer)
                {
                    streetCompleted = false;

                }
            });

            if (!streetCompleted)
                return;

            cells.ForEach(cell =>
            { 
                cell.InStreet = streetCompleted;
            });
        }
    }

    public void RemoveCell()
    {
        nowCellsInStreet--;
        if (nowCellsInStreet < cells.Count)
        {
            cells.ForEach(cell =>
            {
                cell.InStreet = false;
            });
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Set Color")]
    public void SetColorOnCells()
    {
        cells.ForEach(cell =>
        {
            cell.SetColorForStreetImage(streetColor);
        });
    }

    [ContextMenu("Set name street")]
    public void SetNameStreet()
    {
        cells.ForEach(cell =>
        {
            cell.SetNameStreet(NameStreet);
        });
    }

    [ContextMenu("Set new prices")]
    public void SetNewPrice()
    {      
        cells.ForEach(cell =>
        {
            cell.PriceForBuy = defaultPriceBuy;
            cell.PriceForUpgrade = defaultPriceUpgrade;
            cell.PriceOnStay = defaultPriceStayOnCell;
        });
    }
#endif
}
