using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class UIViewPlayer : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private TMP_Text namePlayerField;

    [SerializeField]
    private TMP_Text moneyPlayerField;

    [SerializeField]
    private TMP_Text countCellsField;

    [SerializeField]
    private float activeAlpha,
        hidenAlpha;

    public Player Player { get => player; private set => player = value; }

    private void OnDisable()
    {
        player.OnChangeMoney -= UpdateViewMoney;
    }

    public void Setup(Player newPlayer)
    {
        player = newPlayer;
        namePlayerField.text = newPlayer.Name;
        moneyPlayerField.text = newPlayer.Money.ToString();

        namePlayerField.color = player.ColorPlayer;

        player.OnChangeMoney += UpdateViewMoney;
    }

    public void TurnView(bool isVisible)
    {
        //namePlayerField.fontStyle = isVisible ? FontStyles.Bold : FontStyles.Normal;
        //moneyPlayerField.fontStyle = isVisible ? FontStyles.Bold : FontStyles.Normal;
        //countCellsField.fontStyle = isVisible ? FontStyles.Bold : FontStyles.Normal;

        namePlayerField.color = GetTurnedColor(namePlayerField.color, isVisible);
        moneyPlayerField.color = GetTurnedColor(moneyPlayerField.color, isVisible);
        countCellsField.color = GetTurnedColor(countCellsField.color, isVisible);
    }

    private void Update()
    {
        countCellsField.text = $"Объектов: {player.BuyedCells.Count}";
    }

    private void UpdateViewMoney(int money)
    {
        moneyPlayerField.text = $"Баланс: {money}";
    }

    private Color GetTurnedColor(Color baseColor, bool isVisible)
    {
        Color turnedColor = baseColor;
        turnedColor.a = isVisible ? activeAlpha : hidenAlpha;
        return turnedColor;
    }
}
