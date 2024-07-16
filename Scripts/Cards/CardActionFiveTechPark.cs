using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UERandom = UnityEngine.Random;

public class CardActionFiveTechPark : MonoBehaviour, ICardAction
{

    public void Activate()
    {
        List<Action> listActions = new List<Action>()
        {
            ActionGetMoney,
            ActionGetMoneyFromAllPlayers,
        };

        listActions[UERandom.Range(0, listActions.Count)].Invoke();
    }


    public void ActionGetMoney()
    {
        var reward = UERandom.Range(100, 5000);
        SingletonDataset.Instanse.QueueManager.NowPlayer.Money += reward;
        CreatorMessage.Instanse.CreateMessage("Технопарк", $"Бонус за посещение технопарка: {reward}");
    }

    public void ActionGetMoneyFromAllPlayers()
    {
        var reward = UERandom.Range(100, 1000);
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;

        SingletonDataset.Instanse.QueueManager.Players.ForEach(player =>
        {
            if (player.IdPlayer != activePlayer.IdPlayer)
            {
                player.Money -= reward;
                activePlayer.Money += reward;
            }
        });

        CreatorMessage.Instanse.CreateMessage("Технопарк", $"Каждый игрок решил проспонсировать вас в размере: {reward}");
    }
}
