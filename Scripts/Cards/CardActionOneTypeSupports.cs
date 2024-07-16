using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UERandom = UnityEngine.Random;



public class CardActionOneTypeSupports : MonoBehaviour, ICardAction
{

    public void Activate()
    {
        List<Action> listSupports = new List<Action>()
        {
            GetFirstSupport,
            GetSecondSupport,
            GetThridSupport,
            GetFourthSupport,
            GetFifthSupport,
        };

        listSupports[UERandom.Range(0, listSupports.Count)].Invoke();
    }

    

    private void GetFirstSupport()
    {
        CreatorMessage.Instanse.CreateMessage("���� ���������", "�������� ����� � ������� 1000 ������ �� �������� ������ ��-�������.");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.Money += 1000;
    }

    private void GetSecondSupport()
    {
        CreatorMessage.Instanse.CreateMessage("���� ���������", "��� ����� ����� ������� ����� �� 50 % ��� ��������� �� ���� ���������� ����������.");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.TaxInspectionDiscount += 1;
    }

    private void GetThridSupport()
    {
        CreatorMessage.Instanse.CreateMessage("���� ���������", "���� �������� ������������� �� �������� ��������������� � ������������� �������� �������� 500 ������.");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.Money += 500;
    }

    private void GetFourthSupport()
    {
        CreatorMessage.Instanse.CreateMessage("���� ���������", "������ �������� � ����� ���������� ��������� � ������ �������������� ���");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        SingletonDataset.Instanse.PlayerMovement.ActivePlayerMoved = false;
    }

    private void GetFifthSupport()
    {
        CreatorMessage.Instanse.CreateMessage("���� ���������", "�������� �������������� �������� - �������� 100 ����� �� ��������");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;

        activePlayer.Money += 100;
    }

}
