using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class UIViewActivePlayer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text activePlayerField;

    public void OnChangeActivePlayer(Player player)
    {
        activePlayerField.text = player.Name;
    }
}
