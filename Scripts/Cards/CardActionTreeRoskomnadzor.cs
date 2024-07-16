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
        CreatorMessage.Instanse.CreateMessage("", "������� �� ��������� ��������� ����! ��������� ����� 500 ������");
    }

    private void ActionTwo()
    {
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.InPrison = true;
        activePlayer.DaysInPrison = 1;
        CreatorMessage.Instanse.CreateMessage("", "������������ ������������ ������ � ������ �������� �����. ���������� ���� ���");

    }

    private void ActionThree()
    {
        CreatorMessage.Instanse.CreateMessage("", "������������ �������� ���������� ��������� ��������� � ���������� �����. ��������� 200 ������ �� �������� �� ����������.");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.Money -= 200;
    } 

    private void ActionFour()
    {
        CreatorMessage.Instanse.CreateMessage("", "���� �������� �������� ����� � ������������ ������. ��������� 500 ������");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.Money -= 200;
    }

    private void ActionFive()
    {
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.InPrison = true;
        activePlayer.DaysInPrison = 1;
        CreatorMessage.Instanse.CreateMessage("", "������������ �������� �������� ������������� ������������. ���������� ���� ���");

    }

    private void ActionSix()
    {
        CreatorMessage.Instanse.CreateMessage("", "������ ����� ���������� ������ ����������� � �������������. ��������� 500 ������");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.Money -= 500;
    }
}
