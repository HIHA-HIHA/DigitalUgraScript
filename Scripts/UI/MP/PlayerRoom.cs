
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class PlayerRoom : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nickNameField;

    [SerializeField]
    private Photon.Realtime.Player playerLink;

    public Photon.Realtime.Player PlayerLink { get => playerLink; private set => playerLink = value; }

    public void Setup(string nickname, Photon.Realtime.Player playerLink)
    {
        nickNameField.text = nickname;
        this.PlayerLink = playerLink;
    }
}
