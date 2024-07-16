using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class MPLobbyRoom : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nameRoomField;

    [SerializeField]
    private TMP_Text maxCountPlayersField;

    [SerializeField]
    private TMP_Text countPlayersField;

    public void Setup(string nameRoom, int countPlayers, int maxCountPlayers)
    {
        nameRoomField.text = nameRoom;
        countPlayersField.text = countPlayers.ToString();
        maxCountPlayersField.text = maxCountPlayers.ToString();
    }
}
