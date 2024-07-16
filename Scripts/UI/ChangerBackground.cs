using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class ChangerBackground : MonoBehaviour
{
    [SerializeField]
    private Image background,
        backgroundArt,
        logo,
        line;

    [SerializeField]
    private TMP_Text multiplayer,
        singleplayer,
        nickname;

    [SerializeField]
    private Color defaultColor,
        secondColor;

    [SerializeField]
    private Sprite defaultBackground,
        secondBackground;

    [SerializeField]
    private Sprite defaultLogo,
        secondLogo;

    [SerializeField]
    private Sprite defaultLine,
        secondLine;

    public void TurnView(bool firstMode)
    {
        if (firstMode)
        {
            backgroundArt.gameObject.SetActive(true);
            background.sprite = defaultBackground;
            line.sprite = defaultLine;
            logo.sprite = defaultLogo;

            multiplayer.color = defaultColor;
            singleplayer.color = defaultColor;
            nickname.color = defaultColor;

        }
        else
        {
            backgroundArt.gameObject.SetActive(false);
            background.sprite = secondBackground;
            line.sprite = secondLine;
            logo.sprite = secondLogo;

            multiplayer.color = secondColor;
            singleplayer.color = secondColor;
            nickname.color = secondColor;
        }
    }

}
