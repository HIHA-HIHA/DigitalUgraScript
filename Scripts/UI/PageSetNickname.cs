using Photon.Pun;

using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class PageSetNickname : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputFieldNickname;

    [SerializeField]
    private TMP_Text nickNameTextField;

    public void SetNickname()
    {
        PhotonNetwork.NickName = inputFieldNickname.text;
        nickNameTextField.text = PhotonNetwork.NickName;
    }

    public void TryClosePage()
    {
        if (!string.IsNullOrEmpty(inputFieldNickname.text))
        {
            SetNickname();
            PageManager.Instanse.OpenPage("MainMenu");
        }
    }
}
