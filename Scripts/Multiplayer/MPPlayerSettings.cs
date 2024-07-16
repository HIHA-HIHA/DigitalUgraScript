using Photon.Pun;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPPlayerSettings : MonoBehaviour
{
    

    public void SetNickname(string newNickname)
    {
        PhotonNetwork.NickName = newNickname;    
    }
}
