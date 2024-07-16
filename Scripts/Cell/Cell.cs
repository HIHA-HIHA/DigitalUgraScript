using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEditor;

using UnityEngine;
using UnityEngine.UI;

public enum TypeCell
{
    Start,
    Default,
    Prison,
    Card1,
    Card2,
    Card3,
    Card4,
    Card5,
    Card6
}
public class Cell : MonoBehaviour
{
    #region vals
    [SerializeField]
    private string nameCell;

    [SerializeField]
    private int priceForBuy;

    [SerializeField]
    private int priceOnStay,
        priceOnStayInStreet;

    [SerializeField]
    private int priceForUpgrade;

    private int level = 1;

    [SerializeField]
    private int boost,
        maxBoost;

    [SerializeField]
    private Player owner;

    [SerializeField]
    private bool isBuyed;

    [SerializeField]
    private bool inStreet;

    [SerializeField]
    private Street street;

    [SerializeField]
    private int defaultPriceOnStay;

    [SerializeField]
    private int defaultPriceOnUpgrade;

    [SerializeField]
    private Renderer viewCellRender;

    [SerializeField]
    private Color defaultColor;

    [SerializeField]
    private TypeCell typeCell;

    [SerializeField]
    private Transform[] pointsForPlayers;

    [SerializeField]
    private Image imageColorStreet;

    [SerializeField]
    private TMP_Text nameHomeText;

    [SerializeField]
    private TMP_Text priceText;

    [SerializeField]
    private int businessValue;

    public int PriceForBuy { get => priceForBuy;  set => priceForBuy = value; }
    public int PriceOnStay { get => priceOnStay;   set => priceOnStay = value; }
    public int PriceForUpgrade { get => priceForUpgrade;   set => priceForUpgrade = value; }
    public Player Owner { get => owner;  set => owner = value; }
    public bool IsBuyed { get => isBuyed;  set => isBuyed = value; }
    public bool InStreet { get => inStreet; set => inStreet = value; }
    public TypeCell TypeCell { get => typeCell;  set => typeCell = value; }
    public int BusinessValue { get => businessValue;  set => businessValue = value; }
    public Street Street { get => street;  set => street = value; }
    public string NameCell { get => nameCell; private set => nameCell = value; }



    #endregion

    private void Awake()
    {
        SetDefaultPrice();
    }

    private void SetDefaultPrice()
    {
        if (typeCell != TypeCell.Default)
            return;

        defaultPriceOnStay = PriceOnStay;
        defaultPriceOnUpgrade = PriceForUpgrade;
        businessValue = 0;
        priceOnStay = defaultPriceOnStay;
        priceText.text = priceForBuy.ToString();
    }

    public void SetOwner(Player newOwner)
    {
        if (typeCell == TypeCell.Default) {
            businessValue = priceForBuy;
            Owner = newOwner;
            IsBuyed = true;
            viewCellRender.material.color = newOwner.ColorPlayer;
            street.AddCell();
            priceText.text = priceOnStay.ToString();
        }
    }

    public void RemoveOwner()
    {
        if (typeCell == TypeCell.Default)
        {
            Owner = null;
            IsBuyed = false;
            PriceForUpgrade = defaultPriceOnUpgrade;
            PriceOnStay = defaultPriceOnStay;
            viewCellRender.material.color = defaultColor;
            street.RemoveCell();
            SetDefaultPrice();
        }
    }

    public void Upgrade()
    {
        if (typeCell == TypeCell.Default && priceOnStay <= PriceOnStay-boost)
        {
            businessValue += priceForUpgrade;
            PriceOnStay += boost;
            PriceForUpgrade *= 2;
            level += 1;
            priceText.text = priceOnStay.ToString();
        }
    }

    public void Sell()
    {
        if (typeCell == TypeCell.Default)
        {
            RemoveOwner();
        }
    }

    public Vector3 GetRandomPointForPlayer()
    {
        return pointsForPlayers[Random.Range(0, pointsForPlayers.Length)].position;
    }

    public void SetColorForStreetImage(Color color)
    {
        imageColorStreet.color = color;
    }

    public void SetNameStreet(string street)
    {
        priceText.text = street;
    }

    public void HanlderOnStreet(bool inStreet)
    {
        if(inStreet)
        {
            priceOnStay = priceOnStayInStreet;
        }
        else
        {
            priceOnStay = defaultPriceOnStay;
        }
    }

#if UNITY_EDITOR

    [ContextMenu("Set name")]
    private void SetNameHome()
    {
        if(priceText != null)
            priceText.text = priceForBuy.ToString();

        Undo.RecordObject(gameObject, "Set name home");
        switch (typeCell)
        {
            case TypeCell.Card1:
                nameHomeText.text = "Меры поддержки";
                break;
            case TypeCell.Card2:
                nameHomeText.text = "Налоговая инспекция";
                break;
            case TypeCell.Card3:
                nameHomeText.text = "Технопарк";
                break;
            case TypeCell.Card4:
                nameHomeText.text = "Роскомнадзор";
                break;
            case TypeCell.Card5:
                nameHomeText.text = "Код в мешке";
                break;
            case TypeCell.Card6:
                nameHomeText.text = "ФСТЭК";
                break;
            default:
                nameHomeText.text = nameCell;
                break;
        }
    }

#endif
}
