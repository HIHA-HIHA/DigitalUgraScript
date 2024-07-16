using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovePlayer : MonoBehaviour
{
    [SerializeField]
    private Cell cell;

    public void Run()
    {
        SingletonDataset.Instanse.PlayerMovement.MovePlayer(cell);
        if (SingletonDataset.Instanse.QueueManager.NowPlayer.NowCell != cell)
            Debug.LogError("TEST: ERROR");
        else
            Debug.Log("TEST: ACCES!");
    }
}
