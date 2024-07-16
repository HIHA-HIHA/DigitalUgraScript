using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UERandom = UnityEngine.Random;

public class CardActionSixFSTEK : MonoBehaviour, ICardAction
{
    public void Activate()
    {
        List<Action> actions = new List<Action>()
        {
            ActionOne, ActionTwo, ActionThree, ActionFour, ActionFive, ActionSix,
        };

        actions[UERandom.Range(0, actions.Count)].Invoke();
    }

    private void ActionOne()
    {
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.Money += 500;
        CreatorMessage.Instanse.CreateMessage("", "ѕолучите бонус в размере 500 рублей за обеспечение надежности информационной системы");
    }

    private void ActionTwo()
    {
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.Money += 200;
        CreatorMessage.Instanse.CreateMessage("", "ѕолучите бонус в размере 200 рублей за повышение квалификации персонала");

    }

    private void ActionThree()
    {
        CreatorMessage.Instanse.CreateMessage("", "ѕолучите бонус в размере 200 рублей за защиту конфиденциальной информации");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.Money += 200;
    }

    private void ActionFour()
    {
        CreatorMessage.Instanse.CreateMessage("", "ѕолучите бонус в размере 200 рублей за защиту информации от несанкционированного доступа");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.Money += 200;
    }

    private void ActionFive()
    {
        CreatorMessage.Instanse.CreateMessage("", "ѕолучите бонус в размере 100 рублей за сохранение конфиденциальности данных");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.Money += 100;
    }

    private void ActionSix()
    {
        CreatorMessage.Instanse.CreateMessage("", "ѕолучите бонус в размере 100 рублей за оперативное реагирование на потенциальные угрозы");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.Money += 100;
    }
}

