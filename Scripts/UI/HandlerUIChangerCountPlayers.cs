using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerUIChangerCountPlayers : MonoBehaviour
{
    public void HandlerChangeCountPlayers(int value)
    {
        DatasetCountPlayers.Instanse.CountPlayers = value;
    }
}
