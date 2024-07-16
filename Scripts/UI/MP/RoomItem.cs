using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;


public class RoomItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text roomNameField;

    [SerializeField]
    private TMP_Text countPlayersField;

    [SerializeField]
    private Image iconPassword;

    [SerializeField]
    private RoomInfo roomLink;

    [SerializeField]
    private InputFieldPassword inputFieldPassword;

    private string password;

    private bool hasPassword = true;

    public RoomInfo RoomLink { get => roomLink; private set => roomLink = value; }

    public void Setup(RoomInfo roomLink)
    {
        this.roomLink = roomLink;
        roomNameField.text = roomLink.Name;
        countPlayersField.text = $"{roomLink.PlayerCount}/{roomLink.MaxPlayers}";
        password = (string)roomLink.CustomProperties["password"];

        if (password != "" && !string.IsNullOrEmpty(password))
        {
            iconPassword.gameObject.SetActive(true);
            hasPassword = true;
        }
        else
        {
            iconPassword.gameObject.SetActive(false);
            hasPassword = false;
        }
    }

    public void HandlerClickOnRoom()
    {
        if (!TryConnectToRoom())
        {
            var panel = Instantiate(inputFieldPassword);
            panel.transform.parent = null;
            panel.Setup(this);
        }
    }

    public bool TryConnectToRoom(string password = "")
    {
        if (!hasPassword)
        {
            PhotonNetwork.JoinRoom(roomNameField.text);
            return true;
        }

        if ((string)roomLink.CustomProperties["password"] == password)
        {
            PhotonNetwork.JoinRoom(roomNameField.text);
            return true;
        }
        else
            return false;
    }

    
}
