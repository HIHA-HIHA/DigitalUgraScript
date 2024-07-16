using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    private WinPanel winPanel;

    private int countLosePlayers = 0;

    public static EndGame Instanse;

    private void Awake()
    {
        Instanse = this;
    }

    public void HandlerNoMoney()
    {
        countLosePlayers += 1;

        if (countLosePlayers == DatasetCountPlayers.Instanse.CountPlayers - 1)
        {
            OpenEndGamePanel();
        }
    }

    public void OpenEndGamePanel()
    {
        winPanel.gameObject.SetActive(true);
    }
}
