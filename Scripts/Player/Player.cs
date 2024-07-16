
using System.Collections;

using Hashtable = ExitGames.Client.Photon.Hashtable;

using Photon.Pun;

using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
using UERandom = UnityEngine.Random;

public class Player : MonoBehaviour
{

    #region vars
    [SerializeField]
    private List<GameObject> listPlayerVisualObjects;

    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private Cell nowCell;

    [SerializeField]
    private int idPlayer;

    [SerializeField]
    private int money;

    [SerializeField]
    private int countBuyedCells;

    [SerializeField]
    private string name;

    [SerializeField]
    private Color colorPlayer;

    [SerializeField]
    private bool inPrison;

    [SerializeField]
    private List<Cell> buyedCells;

    [SerializeField]
    private int countGrantsForITCells;

    [SerializeField]
    private int taxInspectionDiscount;

    [SerializeField]
    private int discountOnUpgradeTourismCell;

    [SerializeField]
    private int countDiscountOnBuyCell;

    [SerializeField]
    private int bonus;

    [SerializeField]
    private int daysInPrison;

    [SerializeField]
    private Photon.Realtime.Player playerLink;

    private int lastMoney;

    public bool Lose { get; private set; }

    public Action<int> OnChangeMoney;

    public Action<int> OnChangeCountBuyedCells;

    public int Money
    {
        get => money;
        set
        {
            money = value;

            if (PhotonNetwork.IsConnected && lastMoney != money)
            {
                if (DatasetCountPlayers.Instanse.IsMultiplayer)
                {
                    Hashtable hash = new Hashtable();
                    hash.Add("money", money);
                    hash.Add("link", playerLink);
                    PhotonNetwork.SetPlayerCustomProperties(hash);
                }
            }

            lastMoney = money;
            OnChangeMoney?.Invoke(money);


            if (money <= 100)
            {
                var allPriceSell = 0;
                buyedCells.ForEach(cell =>
                {
                    allPriceSell += cell.PriceForBuy;
                });

                if (allPriceSell < Mathf.Abs(money))
                {
                    EndGame.Instanse.HandlerNoMoney();
                    Lose = true;
                }
            }

        }
    }
    public int IdPlayer { get => idPlayer; set => idPlayer = value; }
    public Cell NowCell { get => nowCell; set => nowCell = value; }
    public string Name { get => name; set => name = value; }
    public Color ColorPlayer { get => colorPlayer; set => colorPlayer = value; }
    public bool InPrison { get => inPrison; set => inPrison = value; }
    public int CountGrantsForITCells { get => countGrantsForITCells; set => countGrantsForITCells = value; }
    public List<Cell> BuyedCells { get => buyedCells; set => buyedCells = value; }
    public int TaxInspectionDiscount { get => taxInspectionDiscount; set => taxInspectionDiscount = value; }
    public int DiscountOnUpgradeTourismCell { get => discountOnUpgradeTourismCell; set => discountOnUpgradeTourismCell = value; }
    public int CountDiscountOnBuyCell { get => countDiscountOnBuyCell; set => countDiscountOnBuyCell = value; }
    public int Bonus { get => bonus; set => bonus = value; }
    public int DaysInPrison { get => daysInPrison; set => daysInPrison = value; }
    public Photon.Realtime.Player PlayerLink { get => playerLink; set => playerLink = value; }

    #endregion

    public int CountBuyedCells
    {
        get => buyedCells.Count; 
    }

    private void Awake()
    {
        BuyedCells = new List<Cell>();

        int idVisualObject = UERandom.Range(0, listPlayerVisualObjects.Count);

        listPlayerVisualObjects.ForEach(view => view.SetActive(false));
        listPlayerVisualObjects[idVisualObject].SetActive(true);

        meshRenderer = listPlayerVisualObjects[idVisualObject].GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        OnChangeCountBuyedCells?.Invoke(CountBuyedCells);
        OnChangeMoney?.Invoke(Money);
    }

    public void AddBuyedCell(Cell cell)
    {
        buyedCells.Add(cell);
        OnChangeCountBuyedCells?.Invoke(CountBuyedCells);
    }

    public void RemoveBuyedCell(Cell cell)
    {
        buyedCells.Remove(cell);
        OnChangeCountBuyedCells?.Invoke(CountBuyedCells);

    }

    public void TurnColor()
    {
        meshRenderer.material.color = colorPlayer;
    }
    
}
