using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UERandom = UnityEngine.Random;
public class CardActionTreeRoskomnadzor : MonoBehaviour, ICardAction
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
        activePlayer.Money -= 500;
        CreatorMessage.Instanse.CreateMessage("", "Попался на нарушении авторских прав! Заплатите штраф 500 рублей");
    }

    private void ActionTwo()
    {
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.InPrison = true;
        activePlayer.DaysInPrison = 1;
        CreatorMessage.Instanse.CreateMessage("", "Роскомнадзор заблокировал доступ к вашему любимому сайту. Пропустите один ход");

    }

    private void ActionThree()
    {
        CreatorMessage.Instanse.CreateMessage("", "Роскомнадзор запретил публикацию некоторых контентов в социальных сетях. Заплатите 200 рублей за лицензию на публикацию.");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.Money -= 200;
    } 

    private void ActionFour()
    {
        CreatorMessage.Instanse.CreateMessage("", "Ваша компания нарушила закон о персональных данных. Заплатите 500 рублей");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.Money -= 200;
    }

    private void ActionFive()
    {
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.InPrison = true;
        activePlayer.DaysInPrison = 1;
        CreatorMessage.Instanse.CreateMessage("", "Роскомнадзор временно запретил использование мессенджеров. Пропустите один ход");

    }

    private void ActionSix()
    {
        CreatorMessage.Instanse.CreateMessage("", "Вашему сайту необходимо пройти регистрацию в Роскомнадзоре. Заплатите 500 рублей");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.Money -= 500;
    }
}
