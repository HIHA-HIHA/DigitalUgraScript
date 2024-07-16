using Photon.Pun;

using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Events;

using Hash = ExitGames.Client.Photon.Hashtable;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private List<Cell> cells;

    [SerializeField]
    private Player activePlayer;

    [SerializeField]
    private UnityEvent<Player,Cell> onStayOnCell;

    [SerializeField]
    private UnityEvent<int, int> onGetStepsForMove;

    private bool activePlayerMoved = false;

    public bool ActivePlayerMoved { get => activePlayerMoved; set => activePlayerMoved = value; }

    public void SetActivePlayer(Player player)
    {
        activePlayerMoved = false;
        activePlayer = player;
    }

    public void MovePlayer()
    {
        if (DatasetCountPlayers.Instanse.IsMultiplayer)
        {
            if (!activePlayer.PlayerLink.IsLocal)
                return;
        }

        if (activePlayerMoved)
            return;

        if (activePlayer  && activePlayer.InPrison)
            return;

        activePlayerMoved = true;
        var steps1 = Random.Range(1, 7);
        var steps2 = Random.Range(1, 7);
        var steps = steps1 + steps2;
        Debug.Log($"{steps}: {steps1} + {steps2}");
        onGetStepsForMove?.Invoke(steps1-1, steps2-1);

        int indexCell = 0;

        if (activePlayer.NowCell)
            indexCell = cells.IndexOf(activePlayer.NowCell);

        int nowStep = 0;
        while (nowStep < steps)
        {
            nowStep++;
            indexCell++;

            if (indexCell >= cells.Count)
                indexCell = 0;

            if (cells[indexCell].TypeCell == TypeCell.Start)
                onStayOnCell?.Invoke(activePlayer, cells[indexCell]);

        }

        StartCoroutine(AnimateMove(cells[indexCell]));

    }


    public void MovePlayer(Cell newCell)
    {

        activePlayerMoved = true;

        StartCoroutine(AnimateMove(newCell));
    }

    /// <summary>
    /// Анимация перемещения игрока
    /// </summary>
    /// <param name="newCell"></param>
    /// <returns></returns>
    private IEnumerator AnimateMove(Cell newCell)
    {
        //Получение индекса текущей клетки
        int indexNowCell = cells.IndexOf(activePlayer.NowCell);

        //Получение индекса новой клетки
        int indexNewCell = cells.IndexOf(newCell);

        //Цикл анимации перемещения
        while (indexNowCell != indexNewCell)
        {
            print("INC: "+indexNowCell);
            indexNowCell++;
            if(indexNowCell > cells.Count -1)
                indexNowCell = 0;

            activePlayer.transform.position = cells[indexNowCell].GetRandomPointForPlayer();
            yield return new WaitForSeconds(0.2f);
        }

        //Установка окончательной клетки
        activePlayer.transform.position = cells[cells.IndexOf(newCell)].GetRandomPointForPlayer();
        activePlayer.NowCell = cells[cells.IndexOf(newCell)];

        //Отправка данных на сихронизации
        if (DatasetCountPlayers.Instanse.IsMultiplayer)
        {
            Hash hash = new();
            hash.Add("link", activePlayer.PlayerLink);
            hash.Add("pos", cells.IndexOf(newCell));
            PhotonNetwork.SetPlayerCustomProperties(hash);
        }

        // Выполнение событий при остановке на клетке
        onStayOnCell?.Invoke(activePlayer, newCell);

    }
}
