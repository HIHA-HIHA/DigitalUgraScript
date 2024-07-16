using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class InputFieldPassword : MonoBehaviour
{
    private RoomItem roomItem;

    [SerializeField]
    private TMP_InputField inputField;

    public void Setup(RoomItem roomItem)
    {
        this.roomItem = roomItem;
    }

    public void EnterPassword() 
    {
        if (roomItem.TryConnectToRoom(inputField.text))
            Destroy(gameObject);
    }

    public void CloseView()
    {
        Destroy(gameObject);
    }
}
