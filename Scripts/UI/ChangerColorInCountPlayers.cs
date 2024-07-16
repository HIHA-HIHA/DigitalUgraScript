using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class ChangerColorInCountPlayers : MonoBehaviour
{
    [SerializeField]
    private TMP_Text[] buttonTexts;

    [SerializeField]
    private Color defaultColor,
        secondColor;

    public void TurnColor(int idPressedButton)
    {
        foreach (var item in buttonTexts)
        {
            item.color = defaultColor;
        }
        buttonTexts[idPressedButton].color = secondColor;
    }
}
